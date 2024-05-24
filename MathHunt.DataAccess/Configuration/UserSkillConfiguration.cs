using MathHunt.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MathHunt.DataAccess.Configuration;

public class UserSkillConfiguration : IEntityTypeConfiguration<UserSkillEntity>
{
    public void Configure(EntityTypeBuilder<UserSkillEntity> builder)
    {
        builder
            .HasKey(s => s.Id);

        builder
            .HasMany(s => s.AppUserEntities)
            .WithMany(u => u.UserSkillsEntities);

        builder
            .Property(s => s.SkillName)
            .IsRequired()
            .HasMaxLength(30);
    }
}