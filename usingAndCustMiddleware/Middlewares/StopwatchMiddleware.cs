using System.Diagnostics;

namespace usingAndCustMiddleware.Middlewares
{
    public class StopwatchMiddleware
    {
        private readonly RequestDelegate _next;

        public StopwatchMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task Invoke(HttpContext context)
        {
            //var stopWatchAlt= new Stopwatch();
            //stopWatchAlt.Start(); 
            var stopWatch=Stopwatch.StartNew();
            await _next(context);
            stopWatch.Stop();
            var miliSecondes = stopWatch.ElapsedMilliseconds; //ne kadar süre geçti?
            await context.Response.WriteAsync($"Harcanan süre {miliSecondes}"); 
        } 
    }
}
