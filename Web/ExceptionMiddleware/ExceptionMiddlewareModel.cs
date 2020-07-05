using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using MongoDB;
using MysqlEntity.Core.Model;

namespace ExceptionMiddleware
{
    public class ExceptionMiddlewareModel
    {
        private readonly RequestDelegate next;
        private readonly ILogger logger;
        private IHostingEnvironment environment;

        public ExceptionMiddlewareModel(RequestDelegate _next, IHostingEnvironment _environment)
        {
            this.next = _next;
            this.environment = _environment;

        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await next.Invoke(context);
                var features = context.Features;
            }
            catch (Exception e)
            {
                await HandleException(context, e);
            }
        }

        private async Task HandleException(HttpContext context, Exception e)
        {
            context.Response.StatusCode = 500;
            context.Response.ContentType = "text/json;charset=utf-8;";
            string error = "";

            void ReadException(Exception ex)
            {
                error += string.Format("{0} | {1} | {2}", ex.Message, ex.StackTrace, ex.InnerException);
                if (ex.InnerException != null)
                {
                    ReadException(ex.InnerException);
                }
            }

            ReadException(e);
            if (environment.IsDevelopment())
            {

                context.Session.TryGetValue("user", out byte[] UserByte);
                if (UserByte == null || UserByte?.Length <= 0)
                {
                    var LoginMsg = new { code = 401, message = "用户登录已过期" };
                    error = Library.Other.JsonToString(LoginMsg);
                  
                }
                else
                {
                    var User = Library.Other.SerializeToObject<Sysuser>(UserByte);
                    MongoDBServer mb = new MongoDBServer();
                    ExceptionLog log = new ExceptionLog()
                    {
                        UserID = User.BillId,
                        UserName = User.UserName,
                        ExceptionMsg = e.Message,
                        ExceptionDetail = error,
                        SysDate = DateTime.Now
                    };
                    mb.db.GetCollection<ExceptionLog>("ExceptionLog").InsertOne(log);
                    var json = new { code = 500, message = e.Message, detail = error };
                    error = Library.Other.JsonToString(json);
                }
            }
            else
                error = "抱歉，出错了";
            await context.Response.WriteAsync(error);
        }
    }
}
