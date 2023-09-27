using Microsoft.EntityFrameworkCore;
using Infrastructure;
using Core;
using WebApi;

var builder = WebApplication.CreateBuilder(args);

string connection = builder.Configuration.GetConnectionString("CinemaConnection") ?? throw new InvalidOperationException("Connection string 'CinemaConnection' not found.");

builder.Services.AddDBContext(connection);

builder.Services.AddControllersWithCustomSchema();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGenWithCustomSchema();

builder.Services.AddIdentity();

builder.Services.AddRepository();

builder.Services.AddValidator();

builder.Services.AddAutoMapper();

builder.Services.AddCustomService();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseMiddleware<ErrorHandlerMiddleware>();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
