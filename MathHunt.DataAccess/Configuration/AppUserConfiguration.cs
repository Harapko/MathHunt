using MathHunt.Core.Models;
using MathHunt.DataAccess.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MathHunt.DataAccess.Configuration;

public class AppUserConfiguration : IEntityTypeConfiguration<AppUserEntity>
{
    public void Configure(EntityTypeBuilder<AppUserEntity> builder)
    {
        builder
            .HasKey(u => u.Id);

        builder
            .HasMany(u => u.UserSkillsEntities)
            .WithOne(us => us.AppUserEntity)
            .HasForeignKey(us=>us.AppUserId);

        builder
            .HasMany(u => u.CompaniesEntity)
            .WithOne(c => c.AppUser)
            .HasForeignKey(c => c.AppUserId);

        builder
            .HasMany(u => u.PhotoUserEntities)
            .WithOne(p => p.AppUserEntity)
            .HasForeignKey(p => p.AppUserEntityId);

        builder
            .Property(u => u.UserName)
            .IsRequired()
            .HasMaxLength(15);
        
        // builder
        //     .Property(u => u.UserSurname)
        //     .IsRequired()
        //     .HasMaxLength(15);
        //
        // builder
        //     .Property(u => u.Email)
        //     .IsRequired()
        //     .HasMaxLength(30);
        //
        // builder
        //     .Property(u => u.PhoneNumber)
        //     .IsRequired()
        //     .HasMaxLength(12);
        //
        // builder
        //     .Property(u => u.UserName)
        //     .IsRequired()
        //     .HasMaxLength(12);
    

    }
}