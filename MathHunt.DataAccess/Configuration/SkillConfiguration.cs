using MathHunt.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MathHunt.DataAccess.Configuration;

public class SkillConfiguration : IEntityTypeConfiguration<SkillEntity>
{
    public void Configure(EntityTypeBuilder<SkillEntity> builder)
    {
        builder
            .HasKey(s => s.Id);

        builder
            .HasMany(s => s.UserSkillEntities)
            .WithOne(us => us.SkillEntity)
            .HasForeignKey(us=>us.SkillId);

        builder
            .Property(s => s.SkillName)
            .IsRequired()
            .HasMaxLength(30);
    }
}