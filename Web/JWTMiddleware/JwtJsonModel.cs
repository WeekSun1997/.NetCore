using EntityFramework.Core.Models;
using Microsoft.IdentityModel.Tokens;
using MysqlEntity.Core.Model;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace JWTMiddleware
{
   public class JwtJsonModel
    {
        public static string JwtKey { get; set; }

        public static string Issuer { get; set; }
        public static string Claims { get; set; }
        /// <summary>
        /// 颁发Token
        /// </summary>
        /// <param name="user">用户</param>
        /// <returns></returns>
        public static dynamic BulidJwtJson(Sysuser user)
        {
            var claims = new[] {

                new Claim(JwtRegisteredClaimNames.Nbf,$"{new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds()}"),
                new Claim(JwtRegisteredClaimNames.Exp,$"{new DateTimeOffset(DateTime.Now.AddMinutes(30)).ToUnixTimeSeconds()}"),
                new Claim(ClaimTypes.Name,user.UserCode)
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                issuer: Issuer,
                audience: Claims,
                claims: claims,
                notBefore: DateTime.Now,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256));
            var jwtToken = new JwtSecurityTokenHandler().WriteToken(token);
            return jwtToken;

        }
    }
}
