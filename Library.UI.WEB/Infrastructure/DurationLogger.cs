using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Library.UI.WEB.Infrastructure
{
    public class DurationLogger
    {
        RequestDelegate _next;
        ILogger<DurationLogger> _logger;

        public DurationLogger(ILogger<DurationLogger> logger, RequestDelegate next)
        {
            _next = next;
            _logger = logger;
        } 

        public async Task Invoke(HttpContext context)
        {
            var sw = new Stopwatch();
            sw.Start();
            await _next.Invoke(context);
            sw.Stop();
            _logger.LogInformation($"{context.Request.Path} Duration {sw.ElapsedMilliseconds}");
        }
    }
}
