using ErrorOr;

namespace AssignmentManagement.Api.Domain.Assignments;

public static class AssignmentErrors
{
    public static readonly Error CannotCompleteAnAssignmentMoreThanOnce = Error.Validation(
        code: "Assignment.CannotCompleteAnAssignmentMoreThanOnce",
        description: "The assignment cannot be completed more than once.");
}