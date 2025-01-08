using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using QuestionBankApp.Data;
using System;


var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<QuestionBankAppContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("AppContext") ?? throw new InvalidOperationException("Connection string 'FirstQnAAPIContext' not found.")));


//Add Cors
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "Api", policy =>
    {
        policy.AllowAnyOrigin()
        .AllowAnyHeader()
        .AllowAnyMethod()
        .WithExposedHeaders("content-disposition")
            .AllowAnyHeader()
            //.AllowCredentials()
            .SetPreflightMaxAge(TimeSpan.FromSeconds(3600));
    });
});

// Add services to the container.

builder.Services.AddControllers();
//builder.Services.AddRazorPages();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseCors("Api");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
//In case of converting ASP.Net Web API project to ASP.Net Web app to use Razor pages to scaffold the dbcontext entities then remove the below line
//app.MapRazorPages();

app.Run();
