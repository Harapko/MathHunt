using MathHunt.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MathHunt.DataAccess.Configuration;

public class CompanyConfiguration : IEntityTypeConfiguration<CompanyEntity>
{
    public void Configure(EntityTypeBuilder<CompanyEntity> builder)
    {
        builder
            .HasKey(c => c.Id);

        builder
            .HasOne(c => c.AppUser)
            .WithMany(u => u.CompaniesEntity)
            .HasForeignKey(c=>c.AppUserId);

        builder
            .HasMany(c => c.CompanySkill)
            .WithOne(cs => cs.Company)
            .HasForeignKey(cs => cs.CompanyId);

        builder
            .Property(c => c.TradeName)
            .HasMaxLength(20)
            .IsRequired();
    }
}