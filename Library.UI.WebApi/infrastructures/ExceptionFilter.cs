using Microsoft.AspNetCore.Mvc;

using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Library.UI.WebApi.infrastructures
{
    public class ExceptionFilter : IAsyncExceptionFilter
    {
        private readonly ILogger<ExceptionFilter> _logger;

        public ExceptionFilter(ILogger<ExceptionFilter> logger)
        {
            _logger = logger;
        }

        public Task OnExceptionAsync(ExceptionContext context)
        {
            _logger.LogError($"Exception of type {context.Exception.GetType().Name} was throw in {context.ActionDescriptor.DisplayName}, Details: {context.Exception}");

            context.Result = new ContentResult()
            {
                Content = $"Exception of type {context.Exception.GetType().Name} was throw in {context.ActionDescriptor.DisplayName}, Details: {context.Exception}",
                StatusCode = 400
            };

            return Task.CompletedTask;
        }
    }
}
