using MathHunt.Application;
using MathHunt.Core.Abstraction.IRepositories;
using MathHunt.Core.Abstraction.IServices;
using MathHunt.Core.Models;
using MathHunt.DataAccess;
using MathHunt.DataAccess.Entities;
using MathHunt.DataAccess.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString(nameof(AppDbContext)));
});


builder.Services
    .AddIdentityApiEndpoints<AppUserEntity>()
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();




builder.Services.AddScoped<IRoleUserService, RoleUserService>();
builder.Services.AddScoped<IRoleUserRepository, RoleUserRepository>();

builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/account/login"; // Путь перенаправления при неудачной аутентификации
});


builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapIdentityApi<AppUserEntity>();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.UseStaticFiles();
app.UseCors();
app.Run();
