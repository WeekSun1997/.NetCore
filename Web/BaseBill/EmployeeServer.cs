using Interface;
using MongoDB;
using MysqlEntity.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB;
using MongoDB.Driver;
using System.Data;
using System.Threading.Tasks;
using AttributeArray;

namespace BaseServer
{
    class EmployeeServer : IEmployee
    {
        public ICache cache { get; set; }
        public IDBServices dBServices { get; set; }
        public EmployeeServer(ICache _cache, IDBServices _dBServices)
        {
            cache = _cache;
            dBServices = _dBServices;
        }

        public object DelEmployee(int EmpID)
        {
            using (var db = new webdevContext())
            {
                var user = db.Employee.Where(a => a.BillId == EmpID).FirstOrDefault();
                string msg = "";
                bool IsSuccess = true;
                if (user == null)
                {
                    IsSuccess = false;
                    IsSuccess = false;
                    msg = "删除的员工信息不存在,请联系管理员确认！";
                }
                else
                {
                    db.Employee.Remove(user);
                    var relurt = db.SaveChangesAsync();
                    msg = relurt.Exception?.Message;
                    if (!string.IsNullOrEmpty(msg))
                    {
                        IsSuccess = false;
                    }
                    else
                    {
                        if (relurt.Result > 0)
                        {
                            cache.Remove("EmployeeList");
                            cache.Remove("Employee_" + user.BillId);
                            MongoDBServer mongo = new MongoDBServer();
                            mongo.db.GetCollection<Employee>("Employee").FindOneAndDelete(a => a.BillId == user.BillId);
                        }
                    }
                }
                return new { IsSuccess = IsSuccess, msg = msg };
            }
        }

        public object DelLeave(int BillID)
        {
            using (webdevContext webdev = new webdevContext())
            {
                string msg = "";
                bool IsSuccess = true;
                var us = webdev.Employeeleave.Where(a => a.BillId == BillID).FirstOrDefault();
                if (us == null)
                {
                    IsSuccess = false;
                    msg = "需要删除的请假单不存在！";
                }
                else
                {
                    try
                    {
                        webdev.Employeeleave.Remove(us);
                        var i = webdev.SaveChanges();
                        if (i > 0)
                        {
                            MongoDBServer mongoDB = new MongoDBServer();
                            mongoDB.db.GetCollection<Employeeleave>("EmployeeLeave").DeleteOne(a => a.BillId == BillID);
                        }
                        else
                        {
                            IsSuccess = false;
                            msg = "发生未知错误";
                        }
                    }
                    catch (Exception ex)
                    {
                        IsSuccess = false;
                        msg = ex.Message;
                    }
                }
                return new { IsSuccess = IsSuccess, msg = msg };
            }
        }

        public object DelPayslip(int BillID)
        {
            using (webdevContext webdev = new webdevContext())
            {
                string msg = "";
                bool IsSuccess = true;
                try
                {
                    var user = webdev.Employeepayslip.Where(a => a.BillId == BillID).FirstOrDefault();
                    if (user == null)
                    {
                        IsSuccess = false;
                        msg = "删除的工资条不存在";
                    }
                    else
                    {
                        webdev.Employeepayslip.Remove(user);
                        int i = webdev.SaveChanges();
                        if (i > 0)
                        {
                            MongoDBServer mongo = new MongoDBServer();
                            var v = mongo.db.GetCollection<Employeepayslip>("Employeepayslip");
                            v.DeleteOne(a => a.BillId == BillID);
                        }
                    }
                }
                catch (Exception ex)
                {

                    msg = ex.Message;
                    IsSuccess = false;
                }
                return new { IsSuccess = IsSuccess, msg = msg };
            }
        }

        public object EmployeeDetail(int EmpId)
        {
            Employee employee;
            string msg = "";
            bool IsSuccess = true;
            if (cache.Exists("Employee_" + EmpId).Result)
            {
                var relust = cache.Get<Employee>("Employee_" + EmpId);
                msg = relust.Exception?.Message;
                if (!string.IsNullOrEmpty(msg))
                {
                    IsSuccess = false;
                }
                employee = relust.Result;
                return new { IsSuccess = IsSuccess, msg = msg, data = employee };
            }
            using (var db = new webdevContext())
            {
                employee = db.Employee.Where(a => a.BillId == EmpId).FirstOrDefault();
                if (employee != null)
                {
                    var r = cache.Set("Employee_" + employee.BillId, employee, TimeSpan.FromHours(2));
                    msg = r.Exception?.Message;
                    if (!string.IsNullOrEmpty(msg))
                    {
                        IsSuccess = false;
                    }
                }
                return new { IsSuccess = IsSuccess, msg = msg, data = employee };
            }
        }

        public object EmployeeLeave()
        {
            string msg = "";
            bool IsSuccess = true;
            List<Employeeleave> list = new List<Employeeleave>();
            try
            {
                MongoDBServer mongo = new MongoDBServer();
                list = mongo.db.GetCollection<Employeeleave>("EmployeeLeave").AsQueryable().ToList();
            }
            catch (Exception ex)
            {
                msg = ex.Message;
                IsSuccess = false;
            }
            return new
            {
                IsSuccess = IsSuccess,
                msg = msg,
                data = list
            };
        }

        public object EmployeeLeaveInsert(Employeeleave employeeLeave)
        {
            using (webdevContext webdev = new webdevContext())
            {
                string msg = "";
                bool IsSuccess = true;
                webdev.Employeeleave.Add(employeeLeave);
                var re = webdev.SaveChangesAsync();
                msg = re.Exception?.Message;
                if (!string.IsNullOrEmpty(msg))
                {
                    IsSuccess = false;
                }
                if (re.Result > 0)
                {
                    MongoDBServer mongoDB = new MongoDBServer();
                    mongoDB.InsertOne<Employeeleave>(employeeLeave, "EmployeeLeave");
                }
                return new { IsSuccess = IsSuccess, msg = msg };
            }

        }

        public object EmployeeList(string IsOnLine)
        {
            List<Employee> le = new List<Employee>();
            string msg = "";
            bool IsSuccess = true;
            try
            {
                if (IsOnLine == "false")
                {
                    if (cache.Exists("EmployeeList").Result)
                    {
                        le = cache.Get<List<Employee>>("EmployeeList").Result;
                    }
                    else
                    {
                        MongoDBServer mongo = new MongoDBServer();
                        le = mongo.db.GetCollection<Employee>("Employee").Aggregate().ToList();
                        cache.Set("EmployeeList", le, TimeSpan.FromHours(2));

                    }
                }
                else
                {
                    if (cache.Exists("EmployeeOnLineList").Result)
                    {
                        le = cache.Get<List<Employee>>("EmployeeOnLineList").Result;
                    }
                    else
                    {
                        MongoDBServer mongo = new MongoDBServer();
                        le = mongo.db.GetCollection<Employee>("Employee").Find(a => a.IsOnLine == true).ToList();
                        cache.Set("EmployeeOnLineList", le, TimeSpan.FromHours(2));
                    }
                }
            }
            catch (Exception ex)
            {
                msg = ex.Message;
                IsSuccess = false;

            }
            return new { IsSuccess = IsSuccess, msg = msg, data = le };

        }

        public object Employeepayslip(Employeepayslip employeepayslip)
        {
            using (webdevContext webdev = new webdevContext())
            {
                string msg = "";
                bool IsSuccess = true;
                try
                {

                    MongoDBServer mongoDB = new MongoDBServer();
                    var model = webdev.Employeepayslip.Where(a => a.BillId == employeepayslip.BillId).FirstOrDefault();
                    if (model != null)
                    {
                        model.BasePay = employeepayslip.BasePay;
                        model.Deduction = employeepayslip.Deduction;
                        model.Subsidy = employeepayslip.Subsidy;
                        model.WagesPayable = employeepayslip.WagesPayable;
                        model.Other = employeepayslip.Other;
                        model.PayDate = employeepayslip.PayDate;
                        model.Remarks = employeepayslip.Remarks;
                        model.UserId = employeepayslip.UserId;
                        webdev.Employeepayslip.Update(model);

                    }
                    else
                    {
                        webdev.Employeepayslip.Add(employeepayslip);

                    }
                    var i = webdev.SaveChanges();
                    if (i <= 0)
                    {
                        msg = "发生未知错误";
                        IsSuccess = false;
                    }
                    if (model != null)
                    {
                        mongoDB.db.GetCollection<Employeepayslip>("Employeepayslip").ReplaceOneAsync(a => a.BillId == employeepayslip.BillId, employeepayslip);
                    }
                    else
                    {
                        mongoDB.InsertOne<Employeepayslip>(employeepayslip, "Employeepayslip");
                    }
                }
                catch (Exception ex)
                {
                    msg = ex.Message;
                    IsSuccess = false;
                }
                return new { IsSuccess = IsSuccess, msg = msg };
            }
        }

        public object payslipList()
        {
            string msg = "";
            bool IsSuccess = true;
            try
            {
                MongoDBServer mongoDB = new MongoDBServer();
                var Employeepayslip = mongoDB.db.GetCollection<Employeepayslip>("Employeepayslip").AsQueryable().ToList();
                var Employee = mongoDB.db.GetCollection<Employee>("Employee").AsQueryable().ToList();
                var s = (from a in Employeepayslip
                         join b in Employee
                         on a.UserId equals b.BillId
                         select new
                         {
                             a.BasePay,
                             a.BillId,
                             a.Deduction,
                             a.Other,
                             a.PayDate,
                             a.Remarks,
                             a.Subsidy,
                             a.UserId,
                             a.WagesPayable,
                             b.UserName
                         }).ToList();
                return new { IsSuccess = IsSuccess, msg = msg, data = s };
            }
            catch (Exception ex)
            {
                msg = ex.Message;
                IsSuccess = false;
                return new { IsSuccess = IsSuccess, msg = msg };
            }
        }

        public object SaveEmployee(Employee employee)
        {
            using (var db = new webdevContext())
            {
                MongoDBServer dBServer = new MongoDBServer();
                string msg = "";
                bool IsSuccess = true;
                var user = db.Employee.Where(a => a.BillId == employee.BillId).FirstOrDefault();
                if (user == null)
                {
                    db.Employee.Add(employee);
                }
                else
                {
                    user.Address = employee.Address;
                    user.BankCard = employee.BankCard;
                    user.BankName = employee.BankName;
                    user.BirthDate = employee.BirthDate;
                    user.EmployeeType = employee.EmployeeType;
                    user.EntryDate = employee.EntryDate;
                    user.IdentityId = employee.IdentityId;
                    user.Zfbid = employee.Zfbid;
                    user.Vxid = employee.Vxid;
                    user.UserName = employee.UserName;
                    user.UrgentPhone = employee.UrgentPhone;
                    user.UrgentName = employee.UrgentName;
                    user.QuitDate = employee.QuitDate;
                    user.IsOnLine = employee.IsOnLine;
                    db.Employee.Update(user);
                }
                var relust = db.SaveChangesAsync();
                msg = relust.Exception?.Message;
                if (!string.IsNullOrEmpty(msg))
                {
                    IsSuccess = false;
                }
                if (relust.Result <= 0)
                {
                    IsSuccess = false;
                }
                if (IsSuccess)
                {

                    if (cache.Exists("EmployeeList").Result)
                    {
                        IsSuccess = cache.Remove("EmployeeList").Result;
                        cache.Remove("EmployeeOnLineList");
                    }
                    if (user != null)
                    {
                        dBServer.db.GetCollection<Employee>("Employee").ReplaceOne(a => a.BillId == employee.BillId, employee);
                        cache.Remove("Employee_" + user.BillId);
                    }
                    else
                    {
                        dBServer.db.GetCollection<Employee>("Employee").InsertOne(employee);
                    }
                }
                return new { IsSuccess = IsSuccess, msg = msg };
            }
        }

        [Cache(invalidData = 30, CacheName = "DownList", CacheType = "Query")]
        public async Task<DataSet> SysDropDown(string DownName = "")
        {
            string sql = "";
            if (string.IsNullOrEmpty(DownName))
            {
                sql = $@"select * from sysdropdown;select  * from sysdropdwondt ";
            }
            else
            {
                sql = $@"select b.id,b.Value from sysdropdown a join sysdropdwondt b on a.billiD=b.billid where a.billName='{DownName}'";
            }
            return await dBServices.QuerySet(sql);
        }

        public async Task<int> UpdateDownListName(int id, string Name)
        {
            using (webdevContext webdev = new webdevContext())
            {

                var sys = webdev.Sysdropdwondt.Where(a => a.Id == id).FirstOrDefault();
                if (sys == null)
                {
                    throw new Exception("下拉不存在");
                }
                else
                {
                    sys.Value = Name;
                    webdev.Sysdropdwondt.Update(sys);
                    await cache.Remove("DownList");
                    return await webdev.SaveChangesAsync();
                }
            }

        }
        public async Task<int> DelDownList(int id)
        {
            using (webdevContext webdev = new webdevContext())
            {

                var sys = webdev.Sysdropdwondt.Where(a => a.Id == id).FirstOrDefault();
                if (sys == null)
                {
                    throw new Exception("下拉不存在");
                }
                else
                {
                    webdev.Sysdropdwondt.Remove(sys);
                    await cache.Remove("DownList");
                    return await webdev.SaveChangesAsync();
                }
            }

        }
        public async Task<int> InsertDownList(int id)
        {
            using (webdevContext webdev = new webdevContext())
            {
                var sys = webdev.Sysdropdown.Where(a => a.BillId == id).FirstOrDefault();
                if (sys == null)
                {
                    throw new Exception("下拉不存在");
                }
                else
                {
                    webdev.Sysdropdwondt.Add(new Sysdropdwondt() { BillId = id, Value = "未命名" });
                    await cache.Remove("DownList");
                    return await webdev.SaveChangesAsync();
                }
            }
        }
    }
}
