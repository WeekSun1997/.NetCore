using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace UI.Controllers
{
    public class EmployeeController : Controller
    {
        public IActionResult EmployeeList()
        {
            return View();
        }
        public IActionResult InsertEmployee(string EmpID)
        {
            ViewData["UserId"] = EmpID;
            return View();
        }
        public IActionResult EmployeeLeave()
        {
            return View();
        }
        public IActionResult InsertLeave ()
        {
            return View();
        }
        public IActionResult EmployeePayslip() {

            return View();
        }
        public IActionResult PayslipList()
        {

            return View();
        }
    }
}