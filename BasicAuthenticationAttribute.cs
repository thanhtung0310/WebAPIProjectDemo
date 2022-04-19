using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using System.Net;
using System.Text;
using System.Threading;
using System.Security.Principal;

namespace WebAPIProjectDemo
{
    public class BasicAuthenticationAttribute: AuthorizationFilterAttribute
    {
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            if (actionContext.Request.Headers.Authorization == null)
            {
                actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized);
            }
            else
            {
                // get token from authorization header request
                string authenticationToken = actionContext.Request.Headers.Authorization.Parameter;

                // decode authentication token
                string decodedAuthenticationToken = Encoding.UTF8.GetString(Convert.FromBase64String(authenticationToken));

                // split the string array
                string[] usernamePwdArray = decodedAuthenticationToken.Split(':');

                // get username && password from string array
                string username = usernamePwdArray[0];
                string pwd = usernamePwdArray[1];

                // set generic principle
                if (EmployeeSecurity.Login(username, pwd))
                {
                    Thread.CurrentPrincipal = new GenericPrincipal(new GenericIdentity(username), null);
                }
                else
                {
                    actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized);
                }
            }
        }
    }
}