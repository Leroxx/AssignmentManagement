using System.Reflection;

using AssignmentManagement.Api.Domain.Assignment;

using Microsoft.EntityFrameworkCore;

namespace AssignmentManagement.Api.Persistance.Common;

public class AssignmentManagementDbContext : DbContext
{
    public DbSet<Assignment> Assignments { get; set; } = null!;
    public AssignmentManagementDbContext(DbContextOptions options) : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(modelBuilder);
    }
}