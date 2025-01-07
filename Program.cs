using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using FirstQnAAPI.Data;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<FirstQnAAPIContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("FirstQnAAPIContext") ?? throw new InvalidOperationException("Connection string 'FirstQnAAPIContext' not found.")));


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
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseCors("Api");
//app.UseCors();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
