namespace AssignmentManagement.Contracts.Assignments;

public record UpdateAssignmentRequest(
    string Name,
    string Description);