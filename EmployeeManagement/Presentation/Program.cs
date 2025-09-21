using Application.Validation;
using Domain.Configs;
using Domain.Entities;
using FluentValidation;
using FluentValidation.AspNetCore;
using Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using Infrastructure.Extensions;
using Presentation.Extensions;

var builder = WebApplication.CreateBuilder(args);

var apiConfig = builder.Configuration.GetSection(nameof(ApiConfig)).Get<ApiConfig>()!;

builder.Services.AddCustomIdentity();

builder.Services.AddCostomAuthentication(apiConfig.ApiSecret);

builder.Services.AddAuthorization();

builder.Services.AddHttpContextAccessor();

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddOpenApi();

builder.Services.AddSwagger();

builder.Services.AddFluentValidationAutoValidation();

builder.Services.AddValidatorsFromAssemblyContaining<EmployeeDtoValidator>();

builder.Services.AddDbContext(builder.Configuration);

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddApiServices(apiConfig);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(options =>
options.WithOrigins(apiConfig.AllowedOrigin.Split(",").Select(origin => origin.Trim()).ToArray())
.AllowAnyMethod()
.AllowAnyHeader()
.WithExposedHeaders("Content-Disposition")
.AllowCredentials());

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    var context = services.GetRequiredService<AppDbContext>();
    var userManager = services.GetRequiredService<UserManager<User>>();

    await DbInitializer.SeedAsync(context, userManager);
}

app.Run();
