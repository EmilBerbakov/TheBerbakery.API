using Microsoft.AspNetCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;
using TheBerbakery.API.Models;
using TheBerbakery.API.Services.Classes;
using TheBerbakery.API.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);
var Configuration = builder.Configuration;

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IRecipeCardService, RecipeCardService>();
builder.Services.AddControllers();
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        //TODO - replace with TheBerbakery's url
        policy.WithOrigins("localhost:4200", "http://localhost:4200", "https:localhost:4200");
    });
});
var connectionString = Configuration.GetSection("Database:ConnectionString").Value;
string[] version = Configuration.GetSection("Database:Version").Value.Split('.').ToArray();
var serverVersion = new MariaDbServerVersion(new Version(Int32.Parse(version[0]), Int32.Parse(version[1]), Int32.Parse(version[2])));

builder.Services.AddDbContext<TheBerbakeryContext>(
    options => options.UseMySql(connectionString, serverVersion)
    );

var app = builder.Build();

//Configure appsettings

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    
}



app.UseExceptionHandler(e =>
{
    e.Run(async context =>
    {
        IExceptionHandlerPathFeature? exceptionHandler = context.Features.Get<IExceptionHandlerPathFeature>();
        string errorPath = $"Error at: {exceptionHandler?.Path}";
        string errorObject = string.Empty;
        if(exceptionHandler != null)
        {
            if (exceptionHandler.Error != null)
            {
                switch (exceptionHandler.Error)
                {
                    case DbUpdateException:
                        var innerException = (SqlException) exceptionHandler.Error.InnerException;
                        switch (innerException.Number)
                        {
                            case 2627:
                            case 547:
                            case 2601:
                                errorObject = Newtonsoft.Json.JsonConvert.SerializeObject(new
                                {
                                    title = errorPath,
                                    status = 409,
                                    message = "The record you attempted to insert conflicts with an existing record.",
                                    detail = innerException.Message,
                                    path = errorPath
                                });
                                break;
                            default:
                                errorObject = Newtonsoft.Json.JsonConvert.SerializeObject(new
                                {
                                    title = errorPath,
                                    status = 400,
                                    message = "Error with request.",
                                    detail = innerException.Message,
                                    path = errorPath
                                });
                                break;
                        }
                        break;
                    default:
                        errorObject = Newtonsoft.Json.JsonConvert.SerializeObject(new
                        {
                            title = errorPath,
                            status = 500,
                            message = "Error. See 'detail' for more information.",
                            detail = exceptionHandler.Error.Message
                        });
                        break;
                }

                await context.Response.WriteAsync(errorObject);

            }
        }
        
    });
});


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseCors();

app.Run();
