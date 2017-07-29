using DigiX.Web.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace DigiX.Web.Api.Filters
{
    public class BasicAuthenticationAttribute : AuthorizationFilterAttribute
    {
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            if (actionContext.Request.Headers.Authorization == null)
            {
                actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized);
            }
            else
            {
                try
                {
                    var authorizationParam = Encoding.UTF8.GetString(Convert.FromBase64String(actionContext.Request.Headers.Authorization.Parameter)).Split(':');

                    var webApiUser = new WebApiUserModel() { UserName = authorizationParam[0], Password = authorizationParam[1] };

                    if (_webApiUsers.FirstOrDefault(it => it.UserName == webApiUser.UserName && it.Password == webApiUser.Password) == null)
                        actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized);
                }
                catch
                {
                    actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized);
                }
            }

            base.OnAuthorization(actionContext);
        }

        private static List<WebApiUserModel> _webApiUsers = new List<WebApiUserModel>()
        {
            new WebApiUserModel() { UserName =  "digix-web-app-1", Password = "digix" },
            new WebApiUserModel() { UserName =  "digix-web-app-2", Password = "digix" }
        };
    }
}