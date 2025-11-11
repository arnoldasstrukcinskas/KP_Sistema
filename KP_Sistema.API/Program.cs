using AutoMapper;
using KP_Sistema.BLL.Interfaces;
using KP_Sistema.BLL.Interfaces.Users;
using KP_Sistema.BLL.Mappings;
using KP_Sistema.BLL.Services;
using KP_Sistema.BLL.Services.Users;
using KP_Sistema.DATA;
using KP_Sistema.DATA.Repositories.Interfaces;
using KP_Sistema.DATA.Repositories.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

//Repositories
builder.Services.AddScoped<IUtilityTaskRepository, UtilityTaskRepository>();
builder.Services.AddScoped<ICommunityRepository, CommunityRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

//Services
builder.Services.AddScoped<IUtilityTaskService, UtilityTaskService>();
builder.Services.AddScoped<ICommunityService, CommunityService>();
builder.Services.AddScoped<IAdministratorService, AdministratorService>();
builder.Services.AddScoped<IManagerService, ManagerService>();
builder.Services.AddScoped<IUserService, UserService>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// AutoMapper Configurations
builder.Services.AddAutoMapper(typeof(Program));


// Configure of swagger documentation
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Order Manager",
        Version = "v1"
    });

    // Include XML comments
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
});

// Enabling healthcheck
builder.Services.AddHealthChecks();

//Get connection string from appsettings.json
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
var serverVersion = new MySqlServerVersion(new Version(9, 1, 0));

builder.Services.AddDbContext<AppDbContext>(
    options => options.UseMySql(connectionString, serverVersion));

var app = builder.Build();

// Try connect to database
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

    try
    {
        await context.Database.CanConnectAsync();
        Console.WriteLine("✅ Database connection successful!");

    }
    catch (Exception ex)
    {
        Console.WriteLine($"❌ Database connection failed: {ex.Message}");
    }

    try
    {
        // Apply migrations automatically
        await context.Database.MigrateAsync();
        Console.WriteLine("✅ Database migrations applied!");

    }
    catch (Exception ex)
    {
        Console.WriteLine($"❌ Database migration failed: {ex.Message}");
    }
}

app.MapHealthChecks("/health");

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
