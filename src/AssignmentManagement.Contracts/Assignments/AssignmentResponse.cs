namespace AssignmentManagement.Contracts.Assignments;

public record AssignmentResponse(
    Guid Id,
    string Name,
    string Description,
    AssignmentStateType State,
    DateTime CreatedDateTime,
    DateTime LastModifiedDateTime);