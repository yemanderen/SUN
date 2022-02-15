using Microsoft.AspNetCore.Cors;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy(name:"Plocy1", builder =>
    {
        builder.WithOrigins("Http://localhost:5058")
        //.WithHeaders("x-cors-header")
        .AllowAnyMethod()
        .AllowAnyHeader()
        //.AllowAnyOrigin()
        ;
    });
});

var app = builder.Build();
app.UseCors("Plocy1");
app.MapGet("/test1", () => "get的结果");
//app.MapGet("/test1", [DisableCors]() => "get的结果");
app.MapPost("/test1", () => "post的结果");
app.MapDelete("/test1", () => "delete的结果");
app.MapPut("/test1", () => "put的结果");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
