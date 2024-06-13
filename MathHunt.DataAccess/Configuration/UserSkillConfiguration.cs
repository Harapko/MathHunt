using MathHunt.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MathHunt.DataAccess.Configuration;

public class UserSkillConfiguration : IEntityTypeConfiguration<UserSkillEntity>
{
    public void Configure(EntityTypeBuilder<UserSkillEntity> builder)
    {
        builder
            .HasKey(us => new { us.AppUserId, us.SkillId });
        
        builder
            .HasOne(us => us.AppUserEntity)
            .WithMany(u => u.UserSkillsEntities)
            .HasForeignKey(us => us.AppUserId);

        builder
            .HasOne(us => us.SkillEntity)
            .WithMany(s => s.UserSkillEntities)
            .HasForeignKey(us => us.SkillId);
    }
}