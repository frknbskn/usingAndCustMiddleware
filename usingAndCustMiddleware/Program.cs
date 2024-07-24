using usingAndCustMiddleware.Extensions;
using usingAndCustMiddleware.Middlewares;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

using var loggerFactory = LoggerFactory.Create(builder =>{ builder.AddSimpleConsole(); });
var logger = loggerFactory.CreateLogger<RequestLoggingMiddleware>();

var app = builder.Build();

//app.Use(async (context, next) =>
//{
//    await context.Response.WriteAsync("=====");
//    await next(); //bir sonraki middleware'e geç.
//    await context.Response.WriteAsync("====");

//});

//app.Use(async (context, next) =>
//{
//    await context.Response.WriteAsync(">>>>");
//    await next(); //bir sonraki middleware'e geç.
//    await context.Response.WriteAsync("<<<<");

//});

//app.UseMiddleware<StopwatchMiddleware>();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}



//app.UseWelcomePage();
app.UseMiddleware<RequestLoggingMiddleware>(logger);
//app.UseMiddleware<JsonBodyMiddleware>();
//app.UseMiddleware<BadWordsHandlerMiddleware>();
app.UseRejectBadWords();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

//app.Run(async context =>
//{
//    await context.Response.WriteAsync("Hello world!");

//});

app.Run();
