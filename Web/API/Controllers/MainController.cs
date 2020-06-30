using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MysqlEntity.Core.Model;

namespace API.Controllers
{
    [Route("api/[controller]")]

    public class MainController : Controller
    {
        public IDBServices db { get; private set; }
        public ICache cache
        {
            get; set;
        }
        public MainController(IDBServices db, ICache _cache)
        {
            this.db = db;
            this.cache = _cache;
        }
        /// <summary>
        /// 好友列表
        /// </summary>     
        /// <returns>好友List</returns>
        [HttpGet("GetFriendList")]
        //[Authorize]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public ActionResult GetFriendList()
        {
            Byte[] UserByte = HttpContext.Session.Get("user");
            int UserId = Library.Other.SerializeToObject<Sysuser>(UserByte).UserId;
            using (webdevContext db = new webdevContext())
            {
                object FriendList = null;
                string msg = "";
                bool success = true;
                try
                {
                    FriendList = (from a in db.Friend
                                  join b in db.Sysuser
                                  on a.FriendId equals b.UserId
                                  where a.UserId == UserId
                                  select new
                                  {
                                      b.UserId,
                                      b.UserName
                                  }).ToList();
                    // FriendList = db.Friend.Join(db.Sysuser,) Where(a => a.UserId == UserId).ToList();
                }
                catch (Exception ex)
                {
                    success = false;
                    msg = ex.Message;
                }
                return Json(new { success = success, msg = msg, data = FriendList });
            }
        }

        /// <summary>
        /// 加载模块列表
        /// </summary>
        /// <returns></returns>
        [HttpGet("LoadSysList")]
        //[Authorize]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public ActionResult LoadSysList()
        {
            try
            {
                DataSet ds;
                string mgs = "";
                string sql = "SELECT Modularid,ModularName,ModularNametext from billmodular;" +
                             "SELECT  a.id,b.Modularid,modularinfoid,ModularInfoulr,ModularInfoname," +
                             "Modulardtnametext  from billmodularinfo  " +
                             "a join billmodular b on a.BillID=b.ID;";
                ds = db.QuerySet(sql).Result;
                return Json(new { IsSuccess = true, data = ds });
            }
            catch (Exception ex)
            {
                return Json(new { IsSuccess = false, msg = ex.Message });
            }
        }

       
    }
}