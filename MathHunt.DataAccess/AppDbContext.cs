using MathHunt.DataAccess.Configuration;
using MathHunt.DataAccess.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace MathHunt.DataAccess;

public class AppDbContext(
    DbContextOptions<AppDbContext> options,
    IConfiguration configuration) : IdentityDbContext<AppUserEntity>(options)
{
    public DbSet<AppUserEntity> AppUsers { get; set; }
    public DbSet<SkillEntity> Skill { get; set; }
    public DbSet<CompanyEntity> Company { get; set; }
    public DbSet<PhotoUserEntity> PhotoUser { get; set; }
    public DbSet<UserSkillEntity> UserSkill { get; set; }
    public DbSet<CompanySkillEntity> CompanySkill { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // modelBuilder.ApplyConfiguration(new AppUserConfiguration());
        // modelBuilder.ApplyConfiguration(new SkillConfiguration());
        // modelBuilder.ApplyConfiguration(new CompanyConfiguration());
        // modelBuilder.ApplyConfiguration(new PhotoUserConfiguration());
        // modelBuilder.ApplyConfiguration(new UserSkillConfiguration());
        // modelBuilder.ApplyConfiguration(new CompanySkillConfiguration());
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder
            .UseNpgsql(configuration.GetConnectionString(nameof(AppDbContext)))
            .UseLoggerFactory(CreateLoggerFactory())
            .EnableSensitiveDataLogging();
    }

    public ILoggerFactory CreateLoggerFactory() =>
        LoggerFactory.Create(builder => { builder.AddConsole(); });
}