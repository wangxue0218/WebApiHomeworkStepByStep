using System;
using System.Net.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace MyApplication
{
    public class LogActionFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            DateTime beforeTime = DateTime.UtcNow;
            actionContext.Request.Properties["StopWatch"] = beforeTime;
            var logger = (IMyLogger) actionContext.Request.GetDependencyScope().GetService(typeof(IMyLogger));
            logger.Info($"Record request begin time for request with url: {actionContext.Request.RequestUri}");
        }

        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            DateTime afterTime = DateTime.UtcNow;
            if (!actionExecutedContext.Request.Properties.ContainsKey("StopWatch") ||
                actionExecutedContext.Request.Properties["StopWatch"] == null) return;

            var beforeTime = (DateTime)actionExecutedContext.Request.Properties["StopWatch"];
            int milliseconds = (afterTime - beforeTime).Milliseconds;
            var logger = (IMyLogger)actionExecutedContext.Request.GetDependencyScope().GetService(typeof(IMyLogger));
            logger.Info($"The request({actionExecutedContext.Request.RequestUri})'s execute time is {milliseconds} seconds");
        }
    }
}