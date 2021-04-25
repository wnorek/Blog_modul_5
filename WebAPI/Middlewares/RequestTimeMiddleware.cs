using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.IO;

namespace WebAPI.Middlewares
{
    public class RequestTimeMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            await next.Invoke(context);

            stopWatch.Stop();
            TimeSpan ts = stopWatch.Elapsed;

            if(ts.Seconds>3)
            {
                using(StreamWriter writer = File.AppendText("RequestTimes.txt"))
                {
                    writer.WriteLine($"Zbyt długi czas oczekiwania : {context.Request.Method} : {context.Request.Path}");
                }
            }

        }
    }
}
