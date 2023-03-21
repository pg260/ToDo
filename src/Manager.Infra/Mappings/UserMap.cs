using Microsoft.EntityFrameworkCore;
using Manager.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Manager.Infra.Mappings;

public class UserMap : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("User");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasColumnName("Id")
            .HasColumnType("VARCHAR(36)");

        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(60)
            .HasColumnName("name")
            .HasColumnType("VARCHAR(60)");

        builder.Property(x => x.Email)
            .IsRequired()
            .HasMaxLength(60)
            .HasColumnName("email")
            .HasColumnType("VARCHAR(60)");

        builder.Property(x => x.Password)
            .IsRequired()
            .HasMaxLength(40)
            .HasColumnName("password")
            .HasColumnType("VARCHAR(40)"); 
    }
}