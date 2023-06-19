var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddCommandLine(args);
var app = builder.Build();

app.MapGet("{**path}", async (HttpContext context, string path) => {
    await context.Response.WriteAsync($"Hello on port {context.Connection.LocalPort} path: {path}");
});
 
app.Run();