namespace CarDealerApp.Filters
{
    using System;
    using System.IO;
    using System.Web.Mvc;

    using CarDealer.Services;
    using Security;

    public class LogAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            var logTimeStamp = DateTime.Now;
            var ipAddress = filterContext.HttpContext.Request.UserHostAddress;
            // When using identity
            // var user = filterContext.HttpContext.User.Identity.Name ?? "Anonymous";
            var username = "Anonymous";
            var sessionCookie = filterContext.HttpContext.Request.Cookies["sessionId"];
            if (sessionCookie != null)
            {
                var sessionId = sessionCookie.Value;
                if (AuthenticationManager.IsAuthenticated(sessionId))
                {
                    username = new Service().GetLoggedUsername(sessionId);
                }
            }

            var controllerName = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName;
            var actionName = filterContext.ActionDescriptor.ActionName;
            var exception = filterContext.Exception;
            var logMessage = $"{logTimeStamp} - {ipAddress} - {username} - {controllerName}.{actionName}";
            if (exception != null)
            {
                var exceptionType = exception.GetType();
                var exceptionMessage = exception.Message;
                logMessage = $"[!] {logMessage} - {exceptionType} - {exceptionMessage}";
            }

            var newLine = Environment.NewLine;
            logMessage = $"{logMessage}{newLine}";

            File.AppendAllText("F:\\Projects\\SU\\CSharp.MVC\\02\\CarDealerApp-Skeleton\\CarDealerApp\\Logs\\logs.txt", logMessage);
        }
    }
}