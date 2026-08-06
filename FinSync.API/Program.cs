using FinSync.API.Extensions;
using FinSync.Application.Features.Agents.Interfaces;
using FinSync.Application.Features.Agents.Services;
using FinSync.Application.Features.Authentication.Interfaces;
using FinSync.Application.Features.Authentication.Services;
using FinSync.Application.Features.Claims.Interfaces;
using FinSync.Application.Features.Claims.Services;
using FinSync.Application.Features.Customers.Interfaces;
using FinSync.Application.Features.Customers.Services;
using FinSync.Application.Features.Customers.Validators;
using FinSync.Application.Features.Dashboard.Interfaces;
using FinSync.Application.Features.Dashboard.Services;
using FinSync.Application.Features.InsuranceCompanies.Interfaces;
using FinSync.Application.Features.InsuranceCompanies.Services;
using FinSync.Application.Features.InsurancePlans.Interfaces;
using FinSync.Application.Features.InsurancePlans.Services;
using FinSync.Application.Features.Policies.Interfaces;
using FinSync.Application.Features.Policies.Services;
using FinSync.Application.Features.PolicyRenewals.Interfaces;
using FinSync.Application.Features.PolicyRenewals.Services;
using FinSync.Application.Features.PremiumPayments.Interfaces;
using FinSync.Application.Features.PremiumPayments.Services;
using FinSync.Application.Features.Reports.Interfaces;
using FinSync.Application.Features.Reports.Services;
using FinSync.Infrastructure.Authentication;
using FinSync.Persistence.Context;
using FinSync.Persistence.Repositories;
using FinSync.Shared.Common;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

using FinSync.Application.Features.Documents.Interfaces;
using FinSync.Application.Features.Documents.Services;

using FinSync.Application.Features.Notifications.Interfaces;
using FinSync.Application.Features.Notifications.Services;

var builder = WebApplication.CreateBuilder(args);


// -----------------------------------------------------
// Controllers & FluentValidation
// -----------------------------------------------------

builder.Services
    .AddControllers()
    .AddFluentValidation(config =>
    {
        config.RegisterValidatorsFromAssemblyContaining<CreateCustomerRequestDtoValidator>();
    });

builder.Services.AddValidatorsFromAssemblyContaining<CreateCustomerRequestDtoValidator>();


// -----------------------------------------------------
// Custom Validation Response
// -----------------------------------------------------

builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.InvalidModelStateResponseFactory = context =>
    {
        var errors = context.ModelState
            .Where(x => x.Value?.Errors.Count > 0)
            .SelectMany(x => x.Value!.Errors)
            .Select(x => x.ErrorMessage)
            .ToList();

        var response = ApiResponseFactory.Failure<object>(
            "Validation failed.",
            errors);

        return new BadRequestObjectResult(response);
    };
});


// -----------------------------------------------------
// Swagger
// -----------------------------------------------------

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Description = "Enter JWT Token",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT"
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Id = "Bearer",
                    Type = ReferenceType.SecurityScheme
                }
            },
            Array.Empty<string>()
        }
    });
});


// -----------------------------------------------------
// Database
// -----------------------------------------------------

builder.Services.AddDbContext<FinSyncDbContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        ServerVersion.AutoDetect(
            builder.Configuration.GetConnectionString("DefaultConnection"))));


// -----------------------------------------------------
// AutoMapper
// -----------------------------------------------------

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());


// -----------------------------------------------------
// CORS (React Frontend)
// -----------------------------------------------------

builder.Services.AddCors(options =>
{
    options.AddPolicy("ReactApp", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});


// -----------------------------------------------------
// JWT Authentication
// -----------------------------------------------------

builder.Services.Configure<JwtSettings>(
    builder.Configuration.GetSection("JwtSettings"));

var jwtSettings = builder.Configuration
    .GetSection("JwtSettings")
    .Get<JwtSettings>();

builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,

            ValidIssuer = jwtSettings!.Issuer,
            ValidAudience = jwtSettings.Audience,

            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(jwtSettings.SecretKey))
        };
    });


// -----------------------------------------------------
// Dependency Injection
// -----------------------------------------------------

// Authentication
builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
builder.Services.AddScoped<IJwtTokenService, JwtTokenService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

// Customer
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<ICustomerService, CustomerService>();

// Agent
builder.Services.AddScoped<IAgentRepository, AgentRepository>();
builder.Services.AddScoped<IAgentService, AgentService>();

// Insurance Company
builder.Services.AddScoped<IInsuranceCompanyRepository, InsuranceCompanyRepository>();
builder.Services.AddScoped<IInsuranceCompanyService, InsuranceCompanyService>();

// Insurance Plan
builder.Services.AddScoped<IInsurancePlanRepository, InsurancePlanRepository>();
builder.Services.AddScoped<IInsurancePlanService, InsurancePlanService>();

// Policy
builder.Services.AddScoped<IPolicyRepository, PolicyRepository>();
builder.Services.AddScoped<IPolicyService, PolicyService>();

// Premium Payment
builder.Services.AddScoped<IPremiumPaymentRepository, PremiumPaymentRepository>();
builder.Services.AddScoped<IPremiumPaymentService, PremiumPaymentService>();

// Policy Renewal
builder.Services.AddScoped<IPolicyRenewalRepository, PolicyRenewalRepository>();
builder.Services.AddScoped<IPolicyRenewalService, PolicyRenewalService>();

// Dashboard
builder.Services.AddScoped<IDashboardRepository, DashboardRepository>();
builder.Services.AddScoped<IDashboardService, DashboardService>();

// Claim
builder.Services.AddScoped<IClaimRepository, ClaimRepository>();
builder.Services.AddScoped<IClaimService, ClaimService>();

// Reports
builder.Services.AddScoped<IReportRepository, ReportRepository>();
builder.Services.AddScoped<IReportService, ReportService>();

// Documents
builder.Services.AddScoped<IDocumentRepository, DocumentRepository>();
builder.Services.AddScoped<IDocumentService, DocumentService>();

// Notifications
builder.Services.AddScoped<INotificationRepository, NotificationRepository>();
builder.Services.AddScoped<INotificationService, NotificationService>();

var app = builder.Build();


// -----------------------------------------------------
// HTTP Pipeline
// -----------------------------------------------------

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCustomExceptionMiddleware();

app.UseHttpsRedirection();

app.UseCors("ReactApp");

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();