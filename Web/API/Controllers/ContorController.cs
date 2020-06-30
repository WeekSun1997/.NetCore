using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using API.Models;
using Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MysqlEntity.Core.Model;
using Reflection;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [Controller]
    public class ContorController : Controller
    {
        public IDBServices dBServices { get; set; }
        public ICache cache { get; set; }
        public IBaseBill BaseBill { get; set; }
        public ContorController(IDBServices dB, ICache _cache, IBaseBill _baseBill)
        {
            this.cache = _cache;
            this.dBServices = dB;
            this.BaseBill = _baseBill;
        }
        [HttpGet("GetTable")]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        /// <summary>
        /// 获取数据库所有表名
        /// </summary>
        /// <returns></returns>
        public ActionResult GetTable()
        {
            string msg = "";
            var IsSuccess = true;
            var R = BaseBill.GetTable();
            if (!string.IsNullOrEmpty(R.Exception?.Message))
            {
                msg = R.Exception?.Message;
                IsSuccess = false;
            }
            return Json(new { IsSuccess = IsSuccess, msg = msg, data = R.Result });
        }

        private DataTable GetAllTable()
        {
            DataTable dt = new DataTable();
            if (cache.Exists("Sysdb").Result)
            {
                return cache.Get<DataTable>("Sysdb").Result;
            }
            else
            {

                string sql = $@" set @rownum=0;
                             select (@rownum := @rownum + 1) AS rownum, r.* from (
                             select  table_name as tableName, 'table' as tableType
                             from information_schema.tables WHERE  table_schema = 'webdev' union all
                             select table_name as viewName , 'view' as tableType 
                             from information_schema.views WHERE  table_schema = 'webdev'
														 )r";
                var rs = dBServices.QueryTable(sql);
                string msg = "";
                msg = rs.Exception?.Message;
                if (string.IsNullOrEmpty(msg))
                {
                    try
                    {
                        bool falg = cache.Set("Sysdb", dBServices.QueryTable(sql).Result, TimeSpan.FromMinutes(30)).Result;
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
                dt = rs.Result;
            }
            return dt;
        }

        [HttpPost("AddTable")]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public ActionResult AddTable(List<TableModel> data, string TableName, string type)
        {
            string sql = "";
            //Redis 缓存所有表 Add
            if (type == "Add")
            {
                DataRow[] dr = GetAllTable().Select("tableName='" + TableName + "'");
                if (dr.Count() > 0 || dr == null)
                {
                    return Json(new { IsSuccess = false, msg = $"表名【{TableName}】已经存在,无法重复创建" });
                }
                sql = "Create Table " + TableName + "(";
                string PrimaryKey = "";
                foreach (var item in data)
                {
                    sql += item.tableName + " " + item.tableType + "(" + item.tableLength + ")";
                    if (item.tableIsNull)
                    {
                        sql += " not null";
                    }
                    if (item.tableIsAuto)
                    {
                        sql += " auto_increment ";
                    }
                    if (item.tableKey)
                    {
                        PrimaryKey += "," + item.tableName;
                    }
                    sql += ",";
                }
                if (!string.IsNullOrEmpty(PrimaryKey))
                {
                    PrimaryKey = "PRIMARY KEY(" + PrimaryKey.Substring(1) + ")";

                }
                else
                {
                    sql = sql.Substring(0, sql.Length - 1);
                }
                sql += PrimaryKey + ")";
            }
            if (type == "Upd")
            {
                DataTable dt = LoadTable(TableName).Result;
                sql += "alter table " + TableName;
                foreach (var item in data)
                {
                    if (!string.IsNullOrEmpty(item.OldtableName))
                    {
                        if (item.OldtableName != item.tableName)
                        {
                            sql += $" CHANGE COLUMN  {item.OldtableName}  {item.tableName} {item.tableType}({(item.tableType == "dateitme" ? "0" : item.tableLength)})";
                        }
                    }
                    else
                    {
                        sql += $" add  column {item.OldtableName} {item.tableName} {item.tableType}({(item.tableType == "dateitme" ? "0" : item.tableLength)})";
                    }
                }
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    var tname = dt.Rows[i]["tableName"].ToString();
                    int count = data.Where(a => a.OldtableName == tname).Count();
                    if (count < 1)
                    {
                        sql += $" drop  column {dt.Rows[i]["tableName"]}";
                    }
                }
            }

            var ds = dBServices.ExecuteNoQuery(sql);
            string msg = string.IsNullOrEmpty(ds.Exception?.Message) ? "" : ds.Exception?.Message;
            bool IsSuccess = true;
            if (!string.IsNullOrEmpty(msg))
            {
                IsSuccess = false;
            }
            if (cache.Exists(TableName).Result)
            {
                cache.Remove(TableName);
            }
            IsSuccess = cache.Remove("Sysdb").Result;
            return Json(new { IsSuccess = IsSuccess, msg = msg });
        }
        /// <summary>
        /// 加载表的名称
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <returns></returns>
        [HttpGet("LoadTableCloums")]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public ActionResult LoadTableCloums(string tableName)
        {
            Task<DataTable> relust = LoadTable(tableName);
            string msg = relust.Exception?.Message;
            bool IsSuccess = true;
            if (!string.IsNullOrEmpty(msg))
            {
                IsSuccess = false;
            }
            IsSuccess = cache.Set(tableName, relust.Result, TimeSpan.FromHours(2)).Result;
            return Json(new { IsSuccess = IsSuccess, msg = msg, data = relust.Result });
        }

        private Task<DataTable> LoadTable(string tableName)
        {
            string sql = $@"select column_name as tableName,
                            data_type as tableType,
                            (case when  COLUMN_Key='PRI' then 'true' else 'false' end)tableKey  ,
                            (case when  EXTRA='auto_increment' then 'true' else 'false' end)tableIsAuto,
                            (case when data_type in('DOUBLE','FLOAT','decimal','int') then NUMERIC_precision else CHARACTER_Maximum_LENGTH end)tableLength,
                            (CASE when IS_NULLable='Yes' THEN 'true' else 'false' end )tableIsNULL
                            from information_schema.columns 
                            where table_name='{tableName}'";
            var relust = dBServices.QueryTable(sql);
            return relust;
        }

        /// <summary>
        /// 删除表
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <returns></returns>
        [HttpPost("DelTable")]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public ActionResult DelTable(string tableName)
        {
            string sql = "Drop Table " + tableName;
            var relust = dBServices.ExecuteNoQuery(sql);
            string msg = relust.Exception?.Message;
            bool IsSuccess = true;
            if (!string.IsNullOrEmpty(msg))
            {
                IsSuccess = false;
            }
            IsSuccess = cache.Remove("Sysdb").Result;
            return Json(new { IsSuccess = IsSuccess, msg = msg });
        }

        /// <summary>
        /// /
        /// </summary>
        /// <param name="Sql"></param>
        /// <param name="SqlType"></param>
        /// <returns></returns>
        [HttpPost("ExceSql")]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public ActionResult ExceSql(string Sql, string SqlType)
        {
            bool IsSuccess = true;
            string msg = "";
            DataSet Dset = new DataSet();
            if (SqlType.ToLower() == "select")
            {
                var reulst = dBServices.QuerySet(Sql);
                msg = reulst.Exception?.Message;
                if (!string.IsNullOrEmpty(msg))
                {
                    IsSuccess = false;
                }
                Dset = reulst.Result;
            }
            else
            {
                var reulst = dBServices.ExecuteNoQuery(Sql);
                msg = reulst.Exception?.Message;
                if (!string.IsNullOrEmpty(msg))
                {
                    IsSuccess = false;
                }
            }
            return Json(new { IsSuccess = IsSuccess, msg = msg, data = Dset });
        }

        /// <summary>
        /// Table模板
        /// </summary>
        /// 
        /// <returns></returns>
        [HttpGet("BaseTable")]
        public ActionResult BaseTable(string BillNo, string SearchStr)
        {
            string msg = "";
            string sql = $"select * from BillModularInfo where id={BillNo}";
            DataTable dt = dBServices.QueryTable(sql).Result;
            string TableName = dt.Rows[0]["ModularInfoName"].ToString();
            sql = $@" select * from BillTableRemask where BillTableName='{TableName}'; 
                                 select * from {TableName} where 1=1";
            if (!string.IsNullOrEmpty(SearchStr))
            {
                sql += " " + SearchStr.Replace("$$", "%");
            }
            sql += "; SELECT * FROM sysdropdwondt";
            DataSet Ds = dBServices.QuerySet(sql).Result;
            foreach (DataRow item in Ds.Tables[0].Rows)
            {
                if (item["FiledType"].ToString() == "Select" && item["BindList"]?.ToString() != "")
                {

                    var Name = item["Remask"].ToString();

                    Ds.Tables[1].Columns.Add(Name + "_Temp");
                    Ds.Tables[1].Columns[Name + "_Temp"].DataType = typeof(string);
                    foreach (DataRow cell in Ds.Tables[1].Rows)
                    {
                        DataRow[] dr = Ds.Tables[2].Select($"id={cell[Name]}");
                        cell[Name + "_Temp"] = dr[0][2];
                    }
                    Ds.Tables[1].Columns.Remove(Name);
                    Ds.Tables[1].Columns[Name + "_Temp"].ColumnName = Name;
                }

            }
            Ds.Tables.Remove(Ds.Tables[2]);
            return Json(new { IsSuccess = true, msg = msg, data = Ds });
        }
        /// <summary>
        /// 删除单据
        /// </summary>
        /// <param name="BillNo">单据No</param>
        /// <param name="BillID">单据id</param>
        /// <returns></returns>
        [HttpPost("BaseTableDel")]
        public ActionResult BaseTableDel(string BillNo, int BillID)
        {
            string msg = "";
            bool IsSuccess = true;
            try
            {
                string sql = $"select * from BillModularInfo where id={BillNo}";
                DataTable dt = dBServices.QueryTable(sql).Result;
                string TableName = dt.Rows[0]["ModularInfoName"].ToString();
                sql = $"Delete from {TableName} where BillID={BillID}";
                dBServices.ExecuteNoQuery(sql);
            }
            catch (Exception ex)
            {
                msg = ex.Message;
                IsSuccess = false;
            }
            return Json(new { IsSuccess = IsSuccess, msg = msg });
        }

        /// <summary>
        /// 表单模板
        /// </summary>
        /// <returns></returns>
        [HttpPost("BaseFrom")]
        public ActionResult BaseFrom(string BillNo, string BillID)
        {
            string msg = "";
            string sql = $"select * from BillModularInfo where id={BillNo}";
            DataTable dt = dBServices.QueryTable(sql).Result;
            string TableName = dt.Rows[0]["ModularInfoName"].ToString();
            sql = $@" select * from BillTableRemask where BillTableName='{TableName}' order by tableIndex";
            DataSet Dset = new DataSet();
            DataTable Ds = dBServices.QueryTable(sql).Result;
            Ds.TableName = "DataList";

            for (int i = 0; i < Ds.Rows.Count; i++)
            {
                string list = Ds.Rows[i]["BindList"].ToString();
                if (!string.IsNullOrEmpty(list))
                {
                    sql = $"select b.value,b.id from sysdropdown a join sysdropdwondt b on a.billid=b.billid where a.billname='{list}' ";
                    DataTable table = dBServices.QueryTable(sql).Result;
                    table.TableName = list.ToLower();
                    Dset.Tables.Add(table.DefaultView.ToTable());
                }
            }
            DataTable data = new DataTable();
            if (!string.IsNullOrEmpty(BillID) && BillID != "0")
            {
                sql = $"select * from {TableName} where billid={BillID}";
                var t = dBServices.QueryTable(sql);
                data = t.Result;
                Ds.Columns.Add("Value");
                for (int i = 0; i < Ds.Rows.Count; i++)
                {
                    for (int j = 0; j < data.Columns.Count; j++)
                    {
                        if (data.Columns[j].ColumnName.ToUpper() == Ds.Rows[i]["Remask"].ToString().ToUpper())
                        {
                            if (Ds.Rows[i]["FiledType"].ToString() == "Date")
                            {
                                if (data.Rows[0][j] != DBNull.Value || data.Rows[0][j].ToString() != "")
                                {
                                    Ds.Rows[i]["Value"] = Convert.ToDateTime(data.Rows[0][j]).ToString("yyyy-MM-dd");
                                }
                            }
                            else
                            {
                                Ds.Rows[i]["Value"] = data.Rows[0][j].ToString();
                            }
                        }
                    }
                }
            }
            Dset.Tables.Add(Ds.DefaultView.ToTable());
            return Json(new { IsSuccess = true, msg = msg, data = Dset });
        }
        /// <summary>
        /// 单据保存
        /// </summary>
        /// <returns></returns>
        [HttpPost("BaseSave")]
        public ActionResult BaseSave(string BillID, string Mb)
        {
            string msg = "";
            bool IsSuccess = true;
            try
            {
                var Parm = Mb.Split("&");
                string sql = $"select * from BillModularInfo where id={BillID}";
                DataTable dt = dBServices.QueryTable(sql).Result;
                string TableName = dt.Rows[0]["ModularInfoId"].ToString();
                var T = TableName.Substring(0, 1).ToUpper();
                T += TableName.Substring(1);
                Assembly assembly = Assembly.Load("MysqlEntity.Core");
                var model = assembly.CreateInstance("MysqlEntity.Core.Model." + T);
                var ModelCol = model.GetType().GetProperties();
                foreach (var item in Parm)
                {
                    var KeyValue = item.Split("=");
                    string Key = KeyValue[0];
                    string Value = KeyValue[1];
                    if (string.IsNullOrEmpty(Value))
                    {
                        continue;
                    }
                    var Col = ModelCol.Where(a => a.Name.ToUpper() == Key.ToUpper()).FirstOrDefault();
                    if (Col != null)
                    {
                        var t = Library.Other.GetCoreType(Col.PropertyType);
                        if (t == typeof(DateTime))
                        {
                            model.GetType().GetProperty(Col.Name)?.SetValue(model, Convert.ToDateTime(Value));
                        }
                        else if (t == typeof(Int32))
                        {
                            model.GetType().GetProperty(Col.Name)?.SetValue(model, Convert.ToInt32(Value));
                        }
                        else if (t == typeof(Decimal))
                        {
                            model.GetType().GetProperty(Col.Name)?.SetValue(model, Convert.ToDecimal(Value));
                        }
                        else
                        {
                            model.GetType().GetProperty(Col.Name)?.SetValue(model, Value);
                        }
                    }
                }
                if (ModelCol.Where(a => a.Name.ToUpper() == "BILLNO").FirstOrDefault() != null)
                {
                    if (string.IsNullOrEmpty(model.GetType().GetProperty("BillNo").GetValue(model)?.ToString()))
                    {
                        string BillNo = DateTime.Now.ToString("yyyyMMddHHmmss");
                        model.GetType().GetProperty("BillNo").SetValue(model, BillNo);
                    }
                }
                using (webdevContext webdev = new webdevContext())
                {
                    var mi2 = webdev.GetType().GetMethod("Set");
                    var genMethod = mi2.MakeGenericMethod(model.GetType());
                    var obj = genMethod.Invoke(webdev, null);
                    var BIllid = model.GetType().GetProperty("BillId").GetValue(model).ToString();
                    if (string.IsNullOrEmpty(BillID) || BIllid == "0")
                    {
                        var add = obj.GetType().GetMethod("Add");
                        var ob = add.Invoke(obj, new object[] { model });
                    }
                    else
                    {
                        var upd = obj.GetType().GetMethod("Update");
                        upd.Invoke(obj, new object[] { model });
                    }
                    var i = webdev.SaveChanges();
                    IsSuccess = i > 0;
                    return Json(new { msg = msg, IsSuccess = IsSuccess });
                }
            }
            catch (Exception ex)
            {

                return Json(new { msg = ex.Message, IsSuccess = false });
            }
        }

        /// <summary>
        /// 单据列表信息
        /// </summary>
        /// <returns></returns>
        [HttpGet("BaseInfo")]
        public ActionResult BaseInfo(int id = 0)
        {
            using (webdevContext web = new webdevContext())
            {
                string msg = "";
                bool IsSuccess = true;
                List<Billmodularinfo> lb = new List<Billmodularinfo>();
                try
                {
                    if (id == 0)
                    {
                        lb = web.Billmodularinfo.OrderBy(a => a.BillId).ToList();
                    }
                    else
                    {
                        lb = web.Billmodularinfo.Where(a => a.Id == id).ToList();
                    }

                }
                catch (Exception ex)
                {
                    msg = ex.Message;
                    IsSuccess = false;

                }
                return Json(new { msg = msg, IsSuccess = IsSuccess, data = lb });
            }

        }
        /// <summary>
        /// 保存单据信息
        /// </summary>
        /// <param name="billmodularinfo"></param>
        /// <returns></returns>
        [HttpPost("BaseBillSave")]
        public ActionResult BaseBillSave(Billmodularinfo billmodularinfo)
        {
            using (webdevContext webdev = new webdevContext())
            {
                string msg = "";
                bool IsSuccess = true;
                try
                {
                    var model = webdev.Billmodularinfo.Where(a => a.Id == billmodularinfo.Id).FirstOrDefault();
                    if (model == null)
                    {
                        webdev.Billmodularinfo.Add(billmodularinfo);
                    }
                    else
                    {
                        model.BillId = billmodularinfo.BillId;
                        model.BillTable = billmodularinfo.BillTable;
                        model.Islist = billmodularinfo.Islist;
                        model.Modulardtnametext = billmodularinfo.Modulardtnametext;
                        model.ModularInfoId = billmodularinfo.ModularInfoId;
                        model.ModularInfoname = billmodularinfo.ModularInfoname;
                        model.ModularInfoulr = billmodularinfo.ModularInfoulr;
                        webdev.Billmodularinfo.Update(model);
                    }
                    IsSuccess = webdev.SaveChanges() > 0;
                    cache.Remove("Sysdb");
                }
                catch (Exception ex)
                {
                    msg = ex.Message;
                    IsSuccess = false;
                }
                return Json(new { msg = msg, IsSuccess = IsSuccess });
            }
        }
        /// <summary>
        /// 表备注
        /// </summary>
        /// <param name="TableName"></param>
        /// <returns></returns>
        [HttpGet("BaseRemask")]
        public ActionResult BaseRemask(string TableName)
        {
            string msg = "";
            bool IsSuccess = true;
            DataSet Dset = new DataSet();
            string sql = $@" select * from billtableremask where billtableName='billtableremask';
                            select * from billtableremask where billtableName='{TableName}'";
            try
            {
                var remask = dBServices.QuerySet(sql);
                msg = remask.Exception?.Message;
                Dset = remask.Result;
                if (!string.IsNullOrEmpty(msg))
                {
                    IsSuccess = false;
                }
            }
            catch (Exception ex)
            {
                msg = ex.Message;
                IsSuccess = false;
            }
            return Json(new { IsSuccess = IsSuccess, msg = msg, data = Dset });
        }
        /// <summary>
        /// 保存备注表
        /// </summary>
        /// <returns></returns>
        [HttpPost("BaseRemaskSave")]
        public ActionResult BaseRemaskSave(string JsonData, string TableName)
        {

            using (webdevContext web = new webdevContext())
            {
                try
                {
                    string msg = "";
                    bool IsSuccess = true;
                    var list = web.Billtableremask.Where(a => a.BillTableName == TableName);
                    foreach (var item in list)
                    {
                        web.Billtableremask.Remove(item);
                    }

                    var json = Library.Other.ObjectToJson<List<Billtableremask>>(JsonData);
                    foreach (var item in json)
                    {
                        web.Billtableremask.Add(item);
                    }
                    IsSuccess = web.SaveChanges() > 0;
                    return Json(new { IsSuccess = IsSuccess, msg = msg });
                }
                catch (Exception ex)
                {
                    return Json(new { IsSuccess = false, msg = ex.Message });
                }
            }

        }

        /// <summary>
        /// 单据ID
        /// </summary>
        /// <param name="BillNo"></param>
        /// <returns></returns>
        [HttpGet("BaseSearch")]
        public ActionResult BaseSearch(string BillNo)
        {
            string msg = "";
            bool IsSuccess = true;
            string sql = $"select * from BillModularInfo where id={BillNo}";
            DataTable dt = dBServices.QueryTable(sql).Result;
            string TableName = dt.Rows[0]["ModularInfoName"].ToString();
            sql = $@" select * from BillTableRemask where BillTableName='{TableName}' order by tableIndex";
            var Result = dBServices.QueryTable(sql);
            msg = Result.Exception?.Message;
            if (!string.IsNullOrEmpty(msg))
            {
                IsSuccess = false;
            }
            return Json(new { msg = msg, IsSuccess = IsSuccess, data = Result.Result });
        }
    }
}