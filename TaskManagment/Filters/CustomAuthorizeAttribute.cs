using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Authentication;

namespace TaskManagment.Filters
{
    public class CustomAuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (!context.HttpContext.User.Identity.IsAuthenticated || context.HttpContext.User.Identity.Name!="admin@experts.ps")
            {
                context.Result = new UnauthorizedResult();

            }
        }
    }
}
