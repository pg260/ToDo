using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Task = Manager.Domain.Entities.Task;

namespace Manager.Infra.Mappings;

public class TasksMap : IEntityTypeConfiguration<Task>
{
    public void Configure(EntityTypeBuilder<Task> builder)
    {
        builder.ToTable("Tasks");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasColumnName("Id")
            .HasColumnType("VARCHAR(36)");

        builder.Property(x => x.UserId)
            .IsRequired()
            .HasColumnName("UserId")
            .HasColumnType("VARCHAR(36)");

        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(60)
            .HasColumnName("Name")
            .HasColumnType("VARCHAR(50)");

        builder.Property(x => x.Description)
            .HasMaxLength(200)
            .HasColumnName("description")
            .HasColumnType("VARCHAR(200)");

        builder.Property(x => x.Concluded)
            .HasColumnName("concluded")
            .HasColumnType("TINYINT(1)");

        builder.Property(x => x.ConcludedAt)
            .HasColumnName("concludedAt")
            .HasColumnType("DATETIME");
        
        builder.Property(x => x.CreatedAt)
            .HasColumnName("createdAt")
            .HasColumnType("DATETIME");

        builder.Property(x => x.UpdatedAt)
            .HasColumnName("updatedAt")
            .HasColumnType("DATETIME");
    }
}