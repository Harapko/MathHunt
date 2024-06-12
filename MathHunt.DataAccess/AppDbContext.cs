using MathHunt.Core.Models;
using MathHunt.DataAccess.Configuration;
using MathHunt.DataAccess.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MathHunt.DataAccess;

public class AppDbContext : IdentityDbContext<AppUserEntity>
{

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
        
    }
    
    public DbSet<AppUserEntity> AppUsers { get; set; }
    public DbSet<UserSkillEntity> UserSkill { get; set; }
    
    public DbSet<CompanyEntity> Company { get; set; }

    public DbSet<PhotoUserEntity> PhotoUser { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new IdentityUserConfiguration());
        modelBuilder.ApplyConfiguration(new UserSkillConfiguration());
        modelBuilder.ApplyConfiguration(new CompanyConfiguration());
        modelBuilder.ApplyConfiguration(new PhotoUserConfiguration());
        base.OnModelCreating(modelBuilder);
    }
}