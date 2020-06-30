using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using UI.Models;

namespace UI.Controllers
{
  
    public class HomeController : Controller
    {
        [HttpGet]
        public IActionResult InsertName(string UserId )
        {
            ViewData["UserId"] = UserId;
           
            return View();
        }

        [HttpPost]
        public IActionResult Index(string UserID, string UserName, string SendName)
        {

            ViewData["SenderID"] = UserID;
            ViewData["ReceiverID"] = SendName;
            ViewData["UserName"] = UserName;                   
            return View("Index");
        }

        public IActionResult Main(string UserName)
        {
            ViewData["UserName"] = UserName;
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
