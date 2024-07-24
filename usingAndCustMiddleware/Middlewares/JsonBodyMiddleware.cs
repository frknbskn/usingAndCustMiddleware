namespace usingAndCustMiddleware.Middlewares
{
    /// <summary>
    /// Bu sınıftan türeyen instance gelen http request metodu post ise ve json içeriyorsa bu datayı context içinde tutacak.
    /// İlk olarak post olup olmadığına sonra json olup olmadığını kontrol edecek.
    /// </summary>
    public class JsonBodyMiddleware
    {
        private readonly RequestDelegate _next;

        public JsonBodyMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task Invoke(HttpContext context) {
            //gelen method post ise ve json içeriyorsa;
            if (context.Request.Method == "POST" && context.Request.ContentType.StartsWith("application/json"))
            {
                using var streamReader = new StreamReader(context.Request.Body); //bütün json datasını al.
                var jsonBody = await streamReader.ReadToEndAsync(); //string'e çevir.
                context.Items["JsonBody"] = jsonBody; //items koleksiyonunda sakla.

            }
            await _next(context);
        }
    }
}
