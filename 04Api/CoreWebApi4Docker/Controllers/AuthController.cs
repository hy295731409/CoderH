using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Domain.Object.Auth;
using Microsoft.AspNetCore.Authorization;
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

        #region 自带的jwt使用
        [HttpPost, Route("RequestToken")]
        public ActionResult RequestToken([FromBody] RequestTokenInput request)
        {
            if (request.Name == null || request.Pwd == null)
                return BadRequest("参数不能为空");

            var token = GetUserToken(request.Name, "TestUser");
            return Content(token);
        }

        [HttpPost, Route("RefreshToken")]
        public ActionResult RefreshToken([FromBody] RefreshToken request)
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
        #endregion

        #region 自定义鉴权
        [AllowAnonymous]
        [HttpGet]
        [Route("nopermission")]
        public IActionResult NoPermission()
        {
            return Forbid("No Permission!");
        }
        /// <summary>
        /// login
        /// </summary>
        /// <param name="userName">只能用user或者</param>
        /// <param name="pwd"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet]
        [Route("auth")]
        public IActionResult Get(string userName, string pwd)
        {
            if (CheckAccount(userName, pwd, out string role))
            {
                //每次登陆动态刷新
                Const.ValidAudience = userName + pwd + DateTime.Now.ToString();
                // push the user’s name into a claim, so we can identify the user later on.
                //这里可以随意加入自定义的参数，key可以自己随便起
                var claims = new[]
                {
                    new Claim(JwtRegisteredClaimNames.Nbf,$"{new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds()}") ,
                    new Claim (JwtRegisteredClaimNames.Exp,$"{new DateTimeOffset(DateTime.Now.AddMinutes(30)).ToUnixTimeSeconds()}"),
                    new Claim(ClaimTypes.NameIdentifier, userName),
                    new Claim("Role", role)
                };
                //sign the token using a secret key.This secret will be shared between your API and anything that needs to check that the token is legit.
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Const.SecurityKey));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                //.NET Core’s JwtSecurityToken class takes on the heavy lifting and actually creates the token.
                var token = new JwtSecurityToken(
                    issuer: Const.Domain, //颁发者
                    audience: Const.ValidAudience,//过期时间
                    expires: DateTime.Now.AddMinutes(30),// 签名证书
                    signingCredentials: creds, //自定义参数
                    claims: claims);
                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token)
                });
            }
            else
            {
                return BadRequest(new { message = "username or password is incorrect." });
            }
        }

        /// <summary>
        /// 模拟登陆校验
        /// </summary>
        private bool CheckAccount(string userName, string pwd, out string role)
        {
            role = "user";
            if (string.IsNullOrEmpty(userName))
                return false;
            if (userName.Equals("admin"))
                role = "admin";
            return true;
        }
        #endregion
    }
}
