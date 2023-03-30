using Manager.Domain.Entities;
using Manager.Infra.Mappings;
using Microsoft.EntityFrameworkCore;
using Task = Manager.Domain.Entities.Task;

namespace Manager.Infra.Context;

public class ManagerContext : DbContext
{
    public ManagerContext()
    {
    }

    public ManagerContext(DbContextOptions<ManagerContext> options) : base(options)
    {
    }
    
    public virtual DbSet<User> Users { get; set; }
    public virtual DbSet<Task> Tasks { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseMySql("server=localhost;user id=root;password=Lab@inf019;database=ToDo",
                Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.32-mysql"), mySqlOptions => mySqlOptions.EnableRetryOnFailure());       
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_0900_ai_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.ApplyConfiguration(new UserMap());
        modelBuilder.ApplyConfiguration(new TasksMap());
    }
}