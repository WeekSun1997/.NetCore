using System;
using System.Collections.Generic;
using System.Text;
using Castle.DynamicProxy;
using Library;
using Microsoft.AspNetCore.Http;
using MysqlEntity.Core.Model;
using Microsoft.AspNetCore.Session;
using System.Linq;
using MongoDB;
using System.Threading.Tasks;
using System.Reflection;
using Newtonsoft.Json;

namespace AutoFacAop
{
    public class LogAop : IInterceptor
    {

        private readonly IHttpContextAccessor _httpContextAccessor;
        private ISession _session => _httpContextAccessor.HttpContext.Session;
        public LogAop(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public void Intercept(IInvocation invocation)
        {
            var dataIntercept = "";
            bool IsSuccess = true;
            _session.TryGetValue("user", out byte[] UserByte);
            if (UserByte == null || UserByte?.Length <= 0)
            {
                throw new Exception("用户登录已过期");
            }
            var User = Library.Other.SerializeToObject<Sysuser>(UserByte);
            string MethodName = invocation.Method.Name;
            DateTime SysDate = DateTime.Now;
            MongoDBServer mongo = new MongoDBServer();
            LogModel log = new LogModel()
            {
                UserID = User.BillId,
                UserName = User.UserName,
                MethodName = MethodName,
                SysDate = DateTime.Now,
                ExceptionMsg = dataIntercept
            };
            mongo.db.GetCollection<LogModel>("SysLog").InsertOneAsync(log);
            invocation.Proceed();
        }



        private void LogEx(Exception ex, ref string dataIntercept)
        {
            if (ex != null)
            {
                //执行的 service 中，捕获异常
                dataIntercept += ($"方法执行中出现异常：{ex.Message + ex.InnerException}\r\n");
            }
        }
        /// <summary>
        /// Task泛型
        /// </summary>
        /// <param name="invocation"></param>
        private void HandleAsyncWithReflection(IInvocation invocation, Action<Exception> finalAction)
        {
            Exception exception = null;
            try
            {
                var resultType = invocation.Method.ReturnType.GetGenericArguments()[0];
                var mi = resultType.GetType().GetMethod(invocation.Method.Name).MakeGenericMethod(resultType);
                invocation.ReturnValue = mi.Invoke(this, new[] { invocation.ReturnValue });
            }
            catch (Exception ex)
            {
                exception = ex;
            }
            finally
            {
                finalAction(exception);
            }
        }
        /// <summary>
        /// 判断是否为异步方法
        /// </summary>
        /// <param name="method"></param>
        /// <returns></returns>
        public static bool IsAsyncMethod(MethodInfo method)
        {
            return (
                method.ReturnType == typeof(Task) ||
                (method.ReturnType.IsGenericType && method.ReturnType.GetGenericTypeDefinition() == typeof(Task<>))
                );
        }
        /// <summary>
        /// Task
        /// </summary>
        /// <param name="actualReturnValue"></param>
        /// <returns></returns>
        public static async Task AwaitTaskWithPostActionAndFinally(Task actualReturnValue, Action<Exception> finalAction)
        {
            Exception exception = null;

            try
            {
                await actualReturnValue;
            }
            catch (Exception ex)
            {
                exception = ex;
            }
            finally
            {
                finalAction(exception);
            }

        }

    }
}
