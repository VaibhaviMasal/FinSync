using FinSync.Application.Features.Customers.Interfaces;
using FinSync.Application.Features.Customers.Mappings;
using FinSync.Application.Features.Customers.Services;
using FinSync.Application.Features.Customers.Validators;

using FinSync.Persistence.Context;
using FinSync.Persistence.Repositories;

using FluentValidation;
using FluentValidation.AspNetCore;

using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// -------------------------------
// Add services to the container
// -------------------------------

builder.Services
    .AddControllers()
    .AddFluentValidation(config =>
    {
        config.RegisterValidatorsFromAssemblyContaining<CreateCustomerRequestDtoValidator>();
    });

// Register all validators from the assembly
builder.Services.AddValidatorsFromAssemblyContaining<CreateCustomerRequestDtoValidator>();

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Dependency Injection
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<ICustomerService, CustomerService>();

// AutoMapper
builder.Services.AddAutoMapper(typeof(CustomerMappingProfile));

// Database Context
builder.Services.AddDbContext<FinSyncDbContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("DefaultConnection"))
    ));

var app = builder.Build();

// -------------------------------
// Configure HTTP Request Pipeline
// -------------------------------

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();