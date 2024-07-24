namespace usingAndCustMiddleware.Middlewares
{
    /// <summary>
    /// Eğer json içinde kötü kelime varsa; response olarak 400 yani BadRequest döndürür ve hata bildirir.
    /// </summary>
    public class BadWordsHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public BadWordsHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task Invoke(HttpContext context)
        {
            //bir önceki middlewareden sonra geleceği için aynı request'ten ve null gelme ihtimali de olduğu için
            //dolusa string'e döndür boşsa null döndür.
            var jsonBody = (string?)context.Items["JsonBody"];

            var badWords = new List<string>()
            {
                "salak","kaka","kötü","pis"
            };
            bool isContainBadWords = badWords.Any(word => jsonBody.Contains(word));
            if (isContainBadWords)
            {
                context.Response.StatusCode = 400;
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsJsonAsync(new { message = "Post'ta kabul edilmeyen kelimeler var." });
                return;
            }
            await _next(context);
        }
    }
}
