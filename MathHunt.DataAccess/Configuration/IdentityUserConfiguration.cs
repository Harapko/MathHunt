using MathHunt.Core.Models;
using MathHunt.DataAccess.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MathHunt.DataAccess.Configuration;

public class IdentityUserConfiguration : IEntityTypeConfiguration<AppUserEntity>
{
    public void Configure(EntityTypeBuilder<AppUserEntity> builder)
    {
        builder
            .HasKey(u => u.Id);

        builder
            .HasMany(u => u.UserSkillsEntities)
            .WithMany(s => s.AppUserEntities);

        // builder
        //     .Property(u => u.UserName)
        //     .IsRequired()
        //     .HasMaxLength(15);
        //
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