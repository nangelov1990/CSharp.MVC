namespace Dashboard.App.Attributes
{
    using System.Linq;
    using System.Web.Mvc;

    public class DashboardAuthorizeAttribute : AuthorizeAttribute
    {
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            var userAuthenticated = filterContext.HttpContext.Request.IsAuthenticated;
            var roles = this.Roles.Split(',');
            var userRoleDoesNotHaveAccessToAction = !roles.Any(filterContext.HttpContext.User.IsInRole);
            if (userAuthenticated &&
                userRoleDoesNotHaveAccessToAction)
            {
                filterContext.Result = new ViewResult()
                {
                    ViewName = "~/Views/Shared/Unauthorized.cshtml"
                };
            }
            else
            {
                base.HandleUnauthorizedRequest(filterContext);
            }
        }
    }
}
