using Microsoft.AspNetCore.Http;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace JWTMiddleware
{
    public class JwtMiddleware
    {
        public readonly RequestDelegate next;

        public JwtMiddleware(RequestDelegate _next)
        {

            this.next = _next;
        }
        public Task Invoke(HttpContext context)
        {
            if (!context.Request.Headers.ContainsKey("Authorization"))
            {
                return next(context);
            }
            var tokenHeader = context.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

            try
            {
                if (tokenHeader.Length >= 128)
                {
                    //Console.WriteLine($"{DateTime.Now} token :{tokenHeader}");
                    var jwtHandler = new JwtSecurityTokenHandler();
                    JwtSecurityToken jwtToken = jwtHandler.ReadJwtToken(tokenHeader);
                    object user;
                    try
                    {

                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                        throw;
                    }
                    return next(context);
                }

            }
            catch (Exception e)
            {
                Console.WriteLine($"{DateTime.Now} middleware wrong:{e.Message}");
            }
            return next(context);
        }
    }
}
