var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddCommandLine(args);
var app = builder.Build();

app.MapFallback(async context => {
    await context.Response.WriteAsync($"Hello on port {context.Connection.LocalPort} path: {context.Request.Path}");
});
 
app.Run();
