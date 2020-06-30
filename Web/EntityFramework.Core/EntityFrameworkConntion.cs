using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EntityFramework.Core
{
  public  class EntityFrameworkConntion
    {
        private readonly RequestDelegate _next;
        public EntityFrameworkConntion(RequestDelegate next) {

            _next = next;
        }
        public async Task Invoke(HttpContext context)
        {
           
            await _next(context);//把context传进去执行下一个中间件
          
        }
    }
}
