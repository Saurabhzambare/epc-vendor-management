using EpcVendorManagement.Web.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace EpcVendorManagement.Web.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(
        DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Department> Departments =>
        Set<Department>();

    protected override void OnModelCreating(
        ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Department>()
            .HasIndex(department => department.Name)
            .IsUnique();
    }
}