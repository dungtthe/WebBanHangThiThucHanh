using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using WebBanHang.Helpers;

namespace WebBanHang.Security
{

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class AdminAuthorizationFilter :Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var httpContext = context.HttpContext;
            var requestPath = httpContext.Request.Path.ToString().ToLower();

            if (requestPath.StartsWith("/admin"))
            {
                var userRole = SessionHelpers.GetRoleName(httpContext);

                if (userRole != RolesConst.Admin)
                {
                    //context.Result = new RedirectToActionResult("Index", "Home", new { area = "Customer" });
                    context.Result = new RedirectToActionResult("Login", "Account", null);

                }
            }
        }
    }
}
