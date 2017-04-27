namespace CarDealerApp.Filters
{
    using System;
    using System.Diagnostics;
    using System.IO;
    using System.Web.Mvc;

    public class TimerAttribute : ActionFilterAttribute
    {
        private readonly Stopwatch _timer = new Stopwatch();

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            this._timer.Start();
        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            this._timer.Stop();

            var elapsedTime = this._timer.Elapsed;
            var logTimeStamp = DateTime.Now;
            var controllerName = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName;
            var actionName = filterContext.ActionDescriptor.ActionName;
            var newLine = Environment.NewLine;
            var logMessage = $"{logTimeStamp} - {controllerName}.{actionName} - {elapsedTime}{newLine}";

            File.AppendAllText("F:\\Projects\\SU\\CSharp.MVC\\02\\CarDealerApp-Skeleton\\CarDealerApp\\Logs\\timeLogs.txt", logMessage);
        }
    }
}