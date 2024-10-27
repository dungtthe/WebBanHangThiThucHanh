using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using WebBanHang.Helpers;

namespace WebBanHang.Security
{

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class AuthenticationFilter : Attribute,IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var httpContext = context.HttpContext;

            var requestPath = httpContext.Request.Path.ToString().ToLower();

            if (requestPath == "/" || requestPath.StartsWith("/home"))
            {
                return;
            }
            var userId = SessionHelpers.GetUserId(httpContext);

            if (userId==null)
            {
                context.Result = new RedirectToActionResult("Login", "Account", null);
            }
        }
    }
}
