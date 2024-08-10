namespace AssignmentManagement.Contracts.Assignments;

public record CreateAssignmentRequest(
    string Name,
    string Description);