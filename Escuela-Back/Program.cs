using Escuela_Back.Installers;
using Microsoft.Extensions.Logging;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowBlazor", policy =>
    {
        policy.WithOrigins("https://localhost:7087")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

builder.Logging.AddLog4Net("log4net.config");

builder.Services.AddControllers();



builder.Services.InstallServicesInAssembly();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();


app.UseHttpsRedirection();

app.UseAuthorization();

app.UseCors("AllowBlazor");

app.MapControllers();

app.Run();
