using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Domain.Object.Auth;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Org.BouncyCastle.Ocsp;

namespace CoreWebApi4Docker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private TokenParameter tokenParameter = new TokenParameter();
        public AuthController()
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json")
                .Build();
            tokenParameter = config.GetSection("tokenParameter").Get<TokenParameter>();
        }

        [HttpPost,Route("RequestToken")]
        public ActionResult RequestToken([FromBody] RequestTokenInput request)
        {
            if (request.Name == null || request.Pwd == null)
                return BadRequest("参数不能为空");

            var token = GetUserToken(request.Name,"TestUser");
            return Content(token);
        }

        [HttpPost,Route("RefreshToken")]
        public ActionResult RefreshToken([FromBody]RefreshToken request)
        {
            if (request.Token == null && request.refreshToken == null)
                return BadRequest("参数不能为空");
            var handler = new JwtSecurityTokenHandler();
            try
            {

                ClaimsPrincipal claim = handler.ValidateToken(request.Token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(tokenParameter.Secret)),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = false,
                }, out SecurityToken securityToken);

                var username = claim.Identity.Name;

                //这儿是生成Token的代码
                var token = GetUserToken(username, "testUser");

                var refreshToken = "654321";

                return Ok(new[] { token, refreshToken });
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// 获取用户的token
        /// </summary>
        /// <param name="name">用户名称</param>
        /// <param name="role">用户角色</param>
        /// <returns>返回token，不能通过限定role ！= 当前role的验证</returns>
        private string GetUserToken(string name, string role)
        {
            //模拟用户关键数据
            var data = JsonConvert.SerializeObject(new UserData() 
            {
                Id = "1",
                Name = name,
                Pwd = "pwdtest",
                Role = role
            });
            var claims = new[]
            {
                new Claim(ClaimTypes.Name,name),
                new Claim(ClaimTypes.UserData,data),
                new Claim(ClaimTypes.Role,role)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenParameter.Secret));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var jwtToken = new JwtSecurityToken(tokenParameter.Issuer, null, claims, expires: DateTime.UtcNow.AddMinutes(tokenParameter.AccessExpiration), signingCredentials: credentials);
            var token = new JwtSecurityTokenHandler().WriteToken(jwtToken);
            return token;
        }
    }
}
