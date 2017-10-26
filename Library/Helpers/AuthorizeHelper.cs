using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web;
using System.Web.Security;
using System.Data.SqlClient;
using System.Security.Principal;

namespace Library.Helpers
{
    public class AuthorizeHelper : AuthorizeAttribute
    {
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            base.OnAuthorization(filterContext);
        }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            var authCookie = httpContext.Request.Cookies[FormsAuthentication.FormsCookieName];
            if(authCookie != null)
            {
                var ticket = FormsAuthentication.Decrypt(authCookie.Value);
                var userdata = ticket.UserData.Split(' ');
                var identity = new GenericIdentity(ticket.Name);
                httpContext.User = new GenericPrincipal(identity, userdata);
            }
            return base.AuthorizeCore(httpContext);
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            base.HandleUnauthorizedRequest(filterContext);
        }

        protected override HttpValidationStatus OnCacheAuthorization(HttpContextBase httpContext)
        {
            return base.OnCacheAuthorization(httpContext);
        }
    }
}