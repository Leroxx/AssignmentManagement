using AssignmentManagement.Api.Domain.Assignments;

using ErrorOr;

namespace AssignmentManagement.Api.Domain.Assignment;

public class Assignment
{
    public Guid Id { get; private set; }
    public string Name { get; private set; } = null!;
    public string Description { get; private set; } = null!;
    public AssignmentStateType State { get; private set; } = null!;
    public DateTime CreatedDateTime { get; private set; }
    public DateTime LastModifiedDateTime { get; private set; }

    public Assignment(
        string name,
        string description,
        AssignmentStateType? state = null,
        Guid? id = null,
        DateTime? createdDateTime = null,
        DateTime? lastModifiedDateTime = null)
    {
        Name = name;
        Description = description;
        State = state ?? AssignmentStateType.InProgress;
        Id = id ?? Guid.NewGuid();
        CreatedDateTime = createdDateTime ?? DateTime.UtcNow;
        LastModifiedDateTime = lastModifiedDateTime ?? DateTime.UtcNow;
    }

    public void Update(string name, string description)
    {
        Name = name;
        Description = description;
        LastModifiedDateTime = DateTime.UtcNow;
    }

    public ErrorOr<Success> Complete()
    {
        if (State == AssignmentStateType.Completed)
        {
            return AssignmentErrors.CannotCompleteAnAssignmentMoreThanOnce;
        }

        State = AssignmentStateType.Completed;
        LastModifiedDateTime = DateTime.UtcNow;

        return Result.Success;
    }

    private Assignment() {}
}
