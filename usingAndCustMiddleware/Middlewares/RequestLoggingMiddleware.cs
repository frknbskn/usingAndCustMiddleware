namespace usingAndCustMiddleware.Middlewares
{
    public class RequestLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        ILogger<RequestLoggingMiddleware> _logger;

        public RequestLoggingMiddleware(RequestDelegate next, ILogger<RequestLoggingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            _logger.LogInformation($"Gelen request methodu: {httpContext.Request.Method}, adresi: {httpContext.Request.Path} ");
            //oluşacak response'un bellekte kopyasını aldık.
            var responseBodyStream = httpContext.Response.Body;
            var responseStream = new MemoryStream();
            httpContext.Response.Body = responseStream;
            
            await _next(httpContext);

            responseStream.Seek(0, SeekOrigin.Begin);
            var responsebody = new StreamReader(responseStream).ReadToEnd();
            _logger.LogInformation($"Response oluşturuldu. \n Yanıt oluştuğu an: {DateTime.Now.ToString()}");
            _logger.LogInformation($"Response: {httpContext.Response.StatusCode}\n {responsebody}");

            responseStream.Seek(0, SeekOrigin.Begin);
            await responseStream.CopyToAsync(responseBodyStream);

        }
    }
}
