using AssignmentManagement.Api.Domain.Assignment;

namespace AssignmentManagement.Api.Services.Common;

public interface IAssignmentRepository
{
    Task<Assignment?> GetByIdAsync(Guid assignmentId);
    Task AddAssignmentAsync(Assignment assignment);
    Task<List<Assignment>> ListAssignmentAsync();
    Task<bool> ExistsAsync(Guid assignmentId);
    Task UpdateAssignmentAsync(Assignment assignment);
    Task DeleteAssignmentAsync(Assignment assignment);
}