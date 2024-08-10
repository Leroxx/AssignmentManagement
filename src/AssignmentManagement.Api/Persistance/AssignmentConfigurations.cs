using AssignmentManagement.Api.Domain.Assignment;
using AssignmentManagement.Api.Domain.Assignments;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AssignmentManagement.Api.Persistance;

public class AssignmentConfigurations : IEntityTypeConfiguration<Assignment>
{
    public void Configure(EntityTypeBuilder<Assignment> builder)
    {
        builder.HasKey(a => a.Id);

        builder.Property(a => a.Id)
            .ValueGeneratedNever();

        builder.Property(a => a.State)
            .HasConversion(
                assignmentStateType => assignmentStateType.Value,
                value => AssignmentStateType.FromValue(value));
    }
}
