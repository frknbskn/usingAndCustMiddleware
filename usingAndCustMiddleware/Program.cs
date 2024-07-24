var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.Use(async (context, next) =>
{
    await context.Response.WriteAsync("=====");
    await next(); //bir sonraki middleware'e ge�.
    await context.Response.WriteAsync("====");

});

app.Use(async (context, next) =>
{
    await context.Response.WriteAsync(">>>>");
    await next(); //bir sonraki middleware'e ge�.
    await context.Response.WriteAsync("<<<<");

});



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    //app.UseSwagger();
    //app.UseSwaggerUI();
}

//app.UseWelcomePage();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run(async context =>
{
    await context.Response.WriteAsync("Hello world!");

});

app.Run();
