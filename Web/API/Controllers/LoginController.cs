using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EntityFramework.Core.Models;
using Microsoft.AspNetCore.Mvc;
using MysqlEntity.Core.Model;
using Microsoft.AspNetCore.Session;
using Microsoft.AspNetCore.Http;
using System.Text;
using Newtonsoft.Json;
using Library;
using Microsoft.AspNetCore.Cors;
using JWTMiddleware;
using MongoDB;

namespace API.Controllers
{
    [Route("api/[controller]")]

    public class LoginController : Controller
    {
        /// <summary>
        /// 用户登录方法
        /// </summary>
        /// <param name="UserName">用户名</param>
        /// <param name="Password">密码</param>
        /// <returns></returns>
        [HttpPost("Login")]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public ActionResult Login(string UserName, string Password)
        {
            using (webdevContext DB = new webdevContext())
            {
                Sysuser us = DB.Sysuser.Where(a => a.UserCode == UserName && a.PassWrod == Password).FirstOrDefault();
                if (us == null)
                    return Json(new { Success = false, msg = "账号或密码有误" });
                else
                {
                    byte[] SessionUser = Other.SerializeToByte(us);
                    HttpContext.Session.Set("user", SessionUser);
                    var token = JwtJsonModel.BulidJwtJson(us);
                    return Json(new { Success = true, token = token, name = us.UserName });
                }
            }
            

        }
        /// <summary>
        /// 用户注册
        /// </summary>
        /// <param name="UserName">用户名</param>
        /// <returns></returns>
        [HttpPost("Register")]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public bool Register(string UserName)
        {
            return true;
        }

    }
}
