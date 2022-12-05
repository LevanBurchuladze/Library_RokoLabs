
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;
using System;

namespace Library.UI.WebApi.infrastructures
{
    public class AuditLogActionFilter : ActionFilterAttribute
    {
        private readonly ILogger<AuditLogActionFilter> _logger;
        public string ActionType { get; set; }

        public AuditLogActionFilter(ILogger<AuditLogActionFilter> logger)
        {
            _logger = logger;
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            // Retrieve the information we need to log
            string routeInfo = GetRouteData(filterContext.RouteData);
            string user = filterContext.HttpContext.User.Identity.Name;

            // Write the information to "Audit Log"
            _logger.LogInformation(String.Format("ActionExecuting - {0} ActionType: {1}; User:{2}; Date:{3}"
              , routeInfo, ActionType, user, DateTime.Now), "Audit Log");

            base.OnActionExecuting(filterContext);
        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            // Retrieve the information we need to log
            string routeInfo = GetRouteData(filterContext.RouteData);
            bool isActionSucceeded = filterContext.Exception == null;
            string user = filterContext.HttpContext.User.Identity.Name;

            // Write the information to "Audit Log"
            _logger.LogInformation(String.Format("ActionExecuted - {0} ActionType: {1}; Executed successfully:{2}; User:{3}; Date:{4}"
              , routeInfo, ActionType, isActionSucceeded, user, DateTime.Now), "Audit Log");

            base.OnActionExecuted(filterContext);
        }

        private string GetRouteData(RouteData routeData)
        {
            var controller = routeData.Values["controller"];
            var action = routeData.Values["action"];

            return String.Format("Controller:{0}; Action:{1};", controller, action);
        }
    }
}
