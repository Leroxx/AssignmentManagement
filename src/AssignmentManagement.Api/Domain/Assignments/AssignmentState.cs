using Ardalis.SmartEnum;

namespace AssignmentManagement.Api.Domain.Assignments;

public class AssignmentStateType : SmartEnum<AssignmentStateType>
{
    public static readonly AssignmentStateType InProgress = new(nameof(InProgress), 0);
    public static readonly AssignmentStateType Completed = new(nameof(Completed), 1);

    public AssignmentStateType(string name, int value) : base(name, value)
    {
    }
}