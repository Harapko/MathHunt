using MathHunt.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MathHunt.DataAccess.Configuration;

public class CompanySkillConfiguration : IEntityTypeConfiguration<CompanySkillEntity>
{
    public void Configure(EntityTypeBuilder<CompanySkillEntity> builder)
    {
        builder
            .HasKey(cs => new { cs.CompanyId, cs.SkillId });

        builder
            .HasOne(cs => cs.Company)
            .WithMany(c => c.CompanySkill)
            .HasForeignKey(cs => cs.CompanyId);

        builder
            .HasOne(cs => cs.Skill)
            .WithMany(s => s.CompanySkill)
            .HasForeignKey(cs => cs.SkillId);
    }
}