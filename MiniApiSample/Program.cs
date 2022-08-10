
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpClient();

var app = builder.Build();

app.MapGet("/test", async (IHttpClientFactory clientFactory) =>
{
    var client = clientFactory.CreateClient();
    var content = await client.GetStringAsync("https://www.google.com");
});

app.Run();