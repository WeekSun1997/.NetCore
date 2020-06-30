using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MongoDB;
using MongoDB.Driver;
using MysqlEntity.Core.Model;

namespace API.Controllers
{
    [Route("api/[controller]")]
    public class EmployeeController : Controller
    {
        /// <summary>
        /// 数据库接口
        /// </summary>
        public readonly IDBServices dBServices;
        /// <summary>
        /// 缓存接口
        /// </summary>
        public readonly ICache cache;
        public readonly IEmployee Iemployee;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_dBServices">数据库服务</param>
        /// <param name="_cache">Redis服务</param>
        public EmployeeController(IDBServices _dBServices, ICache _cache, IEmployee _employee)
        {
            this.cache = _cache;
            this.dBServices = _dBServices;
            this.Iemployee = _employee;
        }
        /// <summary>
        /// 员工列表
        /// </summary>
        /// <returns></returns>
        [HttpGet("EmployeeList")]
        public IActionResult EmployeeList(string IsOnLine = "false")
        {
            var r = Iemployee.EmployeeList(IsOnLine);
            return Json(r);
        }

        /// <summary>
        /// 保存修改员工信息
        /// </summary>
        /// <param name="employee"></param>
        /// <returns></returns>
        [HttpPost("SaveEmployee")]
        public IActionResult SaveEmployee(Employee employee)
        {
            var r = Iemployee.SaveEmployee(employee);
            return Json(r);
        }

        /// <summary>
        /// 删除员工信息
        /// </summary>
        /// <param name="EmpID">员工ID</param>
        /// <returns></returns>
        [HttpPost("DelEmployee")]
        public IActionResult DelEmployee(int EmpID)
        {
            var r = Iemployee.DelEmployee(EmpID);
            return Json(r);
        }
        /// <summary>
        ///获取员工详细信息
        /// </summary>
        /// <param name="EmpId">员工ID</param>
        /// <returns></returns>
        [HttpPost("EmployeeDetail")]
        public IActionResult EmployeeDetail(int EmpId)
        {
            var r = Iemployee.EmployeeDetail(EmpId);
            return Json(r);
        }

        /// <summary>
        /// 请假列表
        /// </summary>
        /// <returns></returns>
        [HttpGet("EmployeeLeave")]
        public IActionResult EmployeeLeave()
        {
            var r = Iemployee.EmployeeLeave();
            return Json(r);
        }

        /// <summary>
        /// 根据名称获取下拉
        /// </summary>
        /// <param name="DownName">下拉名称</param>
        /// <returns></returns>
        [HttpGet("SysDropDown")]
        public IActionResult SysDropDown(string BillID = "")
        {
            var r = Iemployee.SysDropDown(BillID);
            string msg = "";
            bool IsSuucess = true;
            if (!string.IsNullOrEmpty(r.Exception?.Message))
            {
                IsSuucess = false;
                msg = r.Exception?.Message;
            }
            return Json(new { msg = msg, IsSuucess = IsSuucess, data = r.Result });
        }

        /// <summary>
        /// 添加员工请假信息
        /// </summary>
        /// <returns></returns>
        [HttpPost("EmployeeLeaveInsert")]
        public IActionResult EmployeeLeaveInsert(Employeeleave employeeLeave)
        {
            var r = Iemployee.EmployeeLeaveInsert(employeeLeave);
            return Json(r);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="BillID"></param>
        /// <returns></returns>
        [HttpPost("DelLeave")]
        public IActionResult DelLeave(int BillID)
        {
            var r = Iemployee.DelLeave(BillID);
            return Json(r);
        }

        /// <summary>
        /// 员工工资条
        /// </summary>
        /// <param name="employeepayslip"></param>
        /// <returns></returns>
        [HttpPost("Employeepayslip")]
        public IActionResult Employeepayslip(Employeepayslip employeepayslip)
        {
            var r = Iemployee.Employeepayslip(employeepayslip);
            return Json(r);
        }
        /// <summary>
        /// 工资条列表
        /// </summary>
        /// <returns></returns>
        [HttpGet("payslipList")]
        public IActionResult payslipList()
        {
            var r = Iemployee.payslipList();
            return Json(r);
        }

        /// <summary>
        /// 删除工资单
        /// </summary>
        /// <param name="BillID"></param>
        /// <returns></returns>
        [HttpPost("DelPayslip")]
        public IActionResult DelPayslip(int BillID)
        {
            var r = Iemployee.DelPayslip(BillID);
            return Json(r);
        }
        /// <summary>
        /// 修改下拉名称
        /// </summary>
        /// <param name="id">下拉ID</param>
        /// <param name="Name">新下拉名称</param>
        /// <returns></returns>
        [HttpPost("UpdateDownListName")]
        public IActionResult UpdateDownListName(int id, string Name)
        {
            string msg = "";
            bool IsSuccess = true;
            var r = Iemployee.UpdateDownListName(id, Name);
            if (!string.IsNullOrEmpty(r.Exception?.Message))
            {
                IsSuccess = false;
                msg = r.Exception?.Message;
            }
            if (IsSuccess == true)
            {
                IsSuccess = cache.Remove("DownList").Result;
            }

            return Json(new { IsSuccess = IsSuccess, msg = msg });

        }
        /// <summary>
        /// 删除下拉
        /// </summary>
        /// <param name="id">下拉ID</param>
        /// <returns></returns>
        [HttpPost("DelDownList")]
        public IActionResult DelDownList(int id)
        {
            string msg = "";
            bool IsSuccess = true;
            var r = Iemployee.DelDownList(id);
            if (!string.IsNullOrEmpty(r.Exception?.Message))
            {
                IsSuccess = false;
                msg = r.Exception?.Message;
            }
            if (IsSuccess == true)
            {
                IsSuccess = cache.Remove("DownList").Result;
            }
            return Json(new { IsSuccess = IsSuccess, msg = msg });
        }

        /// <summary>
        /// 添加下拉
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost("InsertDownList")]
        public IActionResult InsertDownList(int id) {

            string msg = "";
            bool IsSuccess = true;
            var r = Iemployee.InsertDownList(id);
            if (!string.IsNullOrEmpty(r.Exception?.Message))
            {
                IsSuccess = false;
                msg = r.Exception?.Message;
            }
            if (IsSuccess == true)
            {
                IsSuccess = cache.Remove("DownList").Result;
            }
            return Json(new { IsSuccess = IsSuccess, msg = msg });
        }
    }

}


