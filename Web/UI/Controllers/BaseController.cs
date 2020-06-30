using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace UI.Controllers
{
    public class BaseController : Controller
    {
        public IActionResult BaseFrom( string BillNo,string BillID)
        {
  
            ViewData["BillNo"] = BillNo;
            ViewData["BillID"] = BillID;
            return View();
        }
        public IActionResult BaseTable(string BillNo,string SearchStr)
        {
            ViewData["BillNo"] = BillNo;
            ViewData["SearchStr"] = SearchStr;
            return View();
        }
        public IActionResult BaseSearch(string BillNo)
        {
            ViewData["BillNo"] = BillNo;
            return View();
        }
        public IActionResult SysDownListView() {


            return View();
        }

    }
}