using Domain.Object.Auth;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CoreWebApi4Docker.Controllers
{
    public class ApiControllerBase : ControllerBase
    {
        protected string token { get; private set; }
        #region 方式1（待确认）
        //public ApiControllerBase()
        //{
        //    HttpContext.Request.Headers.TryGetValue("Authorization", out var _token);
        //    if (_token.Count > 0)
        //        token = _token.FirstOrDefault();
        //}

        ///// <summary>
        ///// 放到base控制器里面，如果其他地方要用User就不方便，可以考虑做成一个接口实现，通过Ioc解析接口调用GetUser方法
        ///// </summary>
        //protected UserData CurrentUser
        //{
        //    get
        //    {
        //        var data = new UserData();
        //        var schemeProvider = HttpContext.RequestServices.GetService(typeof(IAuthenticationSchemeProvider)) as IAuthenticationSchemeProvider;
        //        var defaultAuthenticate = schemeProvider.GetDefaultAuthenticateSchemeAsync();
        //        if (defaultAuthenticate != null)
        //        {
        //            var result = HttpContext.AuthenticateAsync(defaultAuthenticate.Result.Name);
        //            var user = result?.Result.Principal;
        //            //1.直接取name
        //            //if (user != null)
        //            //{
        //            //    name = user.Identity.Name;
        //            //}
        //            //2.根据ClaimTypes.Name来取值
        //            var claimsIdentity = user.Identity as ClaimsIdentity;
        //            //var userName = claimsIdentity.FindFirst(ClaimTypes.Name)?.Value;
        //            //3.测试userdata
        //            var userData = claimsIdentity.FindFirst(ClaimTypes.UserData)?.Value;
        //            data = JsonConvert.DeserializeObject<UserData>(userData);
        //        }
        //        return data;
        //    }
        //} 
        #endregion

        protected UserData CurrentUser
        {
            get
            {
                var data = new UserData();
                var userData = User.FindFirst(ClaimTypes.UserData)?.Value;
                data = JsonConvert.DeserializeObject<UserData>(userData);
                return data;
            }
        }
    }
}
