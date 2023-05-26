using Microsoft.EntityFrameworkCore;
using System.Numerics;
using TheBerbakery.API.Models;

var builder = WebApplication.CreateBuilder(args);
var Configuration = builder.Configuration;

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

//Configure appsettings

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    
}

var connectionString = Configuration.GetSection("Database:ConnectionString").Value;
string[] version = Configuration.GetSection("Database:Version").Value.Split('.').ToArray();
var serverVersion = new MariaDbServerVersion(new Version(Int32.Parse(version[0]), Int32.Parse(version[1]), Int32.Parse(version[2])));

builder.Services.AddDbContext<TheBerbakeryContext>(
    options => options.UseMySql(connectionString, serverVersion)
    );

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
