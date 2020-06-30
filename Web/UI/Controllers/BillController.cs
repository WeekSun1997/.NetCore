using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace UI.Controllers
{
    public class BillController : Controller
    {
        public IActionResult BasView()
        {
            return View();
        }
        public IActionResult AddTable(string UpdTable = "", string Type = "")
        {
            ViewData["UpdTable"] = UpdTable;
            ViewData["Type"] = Type;
            return View();
        }
        public IActionResult SqlMagger()
        {

            return View();
        }
        public IActionResult BillInfo()
        {
            return View();
        }
        public IActionResult BillView(string id, string Type)
        {
            ViewData["id"] = id;
            ViewData["Type"] = Type;
            return View();
        }
        public IActionResult BillRemask(string TableName)
        {
            ViewData["TableName"] = TableName;
            return View();
        }

    }


}