using usingAndCustMiddleware.Middlewares;

namespace usingAndCustMiddleware.Extensions
{
    public static class MiddleWareExtensions
    {
        public static IApplicationBuilder UseRejectBadWords(this IApplicationBuilder app)
        {
            app.UseMiddleware<JsonBodyMiddleware>();
            app.UseMiddleware<BadWordsHandlerMiddleware>();
            return app;
        }
    }
}
