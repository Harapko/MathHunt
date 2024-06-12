using MathHunt.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MathHunt.DataAccess.Configuration;

public class PhotoUserConfiguration : IEntityTypeConfiguration<PhotoUserEntity>
{
    public void Configure(EntityTypeBuilder<PhotoUserEntity> builder)
    {
        builder
            .HasKey(p => p.Id);

        builder
            .HasOne(p => p.AppUserEntity)
            .WithMany(u => u.PhotoUserEntities)
            .HasForeignKey(p => p.AppUserEntityId);

        builder
            .Property(p => p.Path)
            .IsRequired();
    }
}