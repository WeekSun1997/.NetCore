using AttributeArray;
using Interface;
using Model;
using MysqlEntity.Core.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using static AttributeArray.CacheAttribute;

namespace BaseServer
{
    public class BaseBill : IBaseBill


    {
        public IDBServices dBServices { get; set; }

        public ICache cache { get; set; }
        public BaseBill(IDBServices _dBServices, ICache _cache)
        {

            cache = _cache;
            dBServices = _dBServices;
        }
        public object AddTable(List<TableModel> data, string TableName, string type)
        {
            string sql = "";
            //Redis 缓存所有表 Add
            if (type == "Add")
            {
                DataRow[] dr = GetAllTable().Result.Select("tableName='" + TableName + "'");
                if (dr.Count() > 0 || dr == null)
                {
                    return new { IsSuccess = false, msg = $"表名【{TableName}】已经存在,无法重复创建" };
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
            return new { IsSuccess = IsSuccess, msg = msg };
        }

        private async Task<DataTable> GetAllTable()
        {
            string sql = $@" set @rownum=0;
                             select (@rownum := @rownum + 1) AS rownum, r.* from (
                             select  table_name as tableName, 'table' as tableType
                             from information_schema.tables WHERE  table_schema = 'webdev' union all
                             select table_name as viewName , 'view' as tableType 
                             from information_schema.views WHERE  table_schema = 'webdev'
														 )r";
            return await dBServices.QueryTable(sql);
        }

        public object BaseBillSave(Billmodularinfo billmodularinfo)
        {
            throw new NotImplementedException();
        }

        public object BaseFrom(string BillNo, string BillID)
        {
            throw new NotImplementedException();
        }

        public object BaseInfo(int id = 0)
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
                return new { msg = msg, IsSuccess = IsSuccess, data = lb };
            }

        }

        public object BaseRemask(string TableName)
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
            return new { IsSuccess = IsSuccess, msg = msg, data = Dset };
        }

        public object BaseRemaskSave(string JsonData, string TableName)
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
                    return new { IsSuccess = IsSuccess, msg = msg };
                }
                catch (Exception ex)
                {
                    return new { IsSuccess = false, msg = ex.Message };
                }
            }
        }

        public object BaseSave(string BillID, string Mb)
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
                    return new { msg = msg, IsSuccess = IsSuccess };
                }
            }
            catch (Exception ex)
            {

                return new { msg = ex.Message, IsSuccess = false };
            }
        }

        public object BaseSearch(string BillNo)
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
            return new { msg = msg, IsSuccess = IsSuccess, data = Result.Result };
        }

        public object BaseTable(string BillNo, string SearchStr)
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
            DataSet Ds = dBServices.QuerySet(sql).Result;

            return new { IsSuccess = true, msg = msg, data = Ds };
        }

        public object BaseTableDel(string BillNo, int BillID)
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
            return new { IsSuccess = IsSuccess, msg = msg };
        }

        public object DelTable(string tableName)
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
            return new { IsSuccess = IsSuccess, msg = msg };
        }

        public object ExceSql(string Sql, string SqlType)
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
            return new { IsSuccess = IsSuccess, msg = msg, data = Dset };
        }

        [Cache(invalidData = 30, CacheName = "Sysdb",CacheType ="Query")]
        public Task<DataTable> GetTable()
        {
            return GetAllTable();
        }

        public Task<DataTable> LoadTable(string tableName)
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

        public object LoadTableCloums(string tableName)
        {
            Task<DataTable> relust = LoadTable(tableName);
            string msg = relust.Exception?.Message;
            bool IsSuccess = true;
            if (!string.IsNullOrEmpty(msg))
            {
                IsSuccess = false;
            }
            IsSuccess = cache.Set(tableName, relust.Result, TimeSpan.FromHours(2)).Result;
            return new { IsSuccess = IsSuccess, msg = msg, data = relust.Result };
        }
    }
}