using System.Text;
using System.Text.Json.Serialization;
using MathHunt.Application;
using MathHunt.Core.Abstraction.IRepositories;
using MathHunt.Core.Abstraction.IServices;
using MathHunt.DataAccess;
using MathHunt.DataAccess.Entities;
using MathHunt.DataAccess.Repositories;
using Microsoft.AspNetCore.Authentication.BearerToken;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(o =>
{
    o.AddSecurityDefinition("BearerAuth", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Enter proper JWT token",
        Name = "Authorization",
        Scheme = "Bearer",
        BearerFormat = "JWT",
        Type = SecuritySchemeType.Http
    });
    o.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "BearerAuth"
                }
            },
            Array.Empty<string>()
        }
    });
    
    
});

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString(nameof(AppDbContext)));
});

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.Events.OnRedirectToLogin = (context) =>
        {
            context.Response.StatusCode = 401;
            return Task.CompletedTask;
        };
    });

builder.Services.AddAuthorization();


builder.Services
    .AddIdentityApiEndpoints<AppUserEntity>()
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<AppDbContext>();

builder.Services.AddSingleton<IHttpContextAccessor,
    HttpContextAccessor>();

builder.Services.AddScoped<IRoleUserService, RoleUserService>();
builder.Services.AddScoped<IRoleUserRepository, RoleUserRepository>();

builder.Services.AddScoped<ISkillUserService, SkillUserService>();
builder.Services.AddScoped<ISkillUserRepository, SkillUserRepository>();

builder.Services.AddScoped<IAppUserService, AppUserService>();
builder.Services.AddScoped<IAppUserRepository, AppUserRepository>();

builder.Services.AddScoped<ICompanyService, CompanyService>();
builder.Services.AddScoped<ICompanyRepository, CompanyRepository>();

builder.Services.AddScoped<IUserManagerService, UserManagerService>();
builder.Services.AddScoped<IUserManagerRepository, UserManagerRepository>();


builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/account/login"; // Путь перенаправления при неудачной аутентификации
});


builder.Services.AddControllers();



builder.Services.AddCors(opt =>
{
    opt.AddPolicy("CorsPolicy", policyBilder =>
    {
        policyBilder
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials()
            .WithOrigins("http://localhost:4200");
    });
});


var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseStaticFiles();
app.UseCors("CorsPolicy");
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.MapIdentityApi<AppUserEntity>();
app.Run();