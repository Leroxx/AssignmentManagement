using AssignmentManagement.Api.Domain.Assignment;
using AssignmentManagement.Api.Persistance.Common;
using AssignmentManagement.Api.Services.Common;
using Microsoft.EntityFrameworkCore;

namespace AssignmentManagement.Api.Persistance;

public class AssignmentRepository : IAssignmentRepository
{
    private readonly AssignmentManagementDbContext _dbContext;

    public AssignmentRepository(AssignmentManagementDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddAssignmentAsync(Assignment assignment)
    {
        await _dbContext.Assignments.AddAsync(assignment);
        await _dbContext.SaveChangesAsync();
    }

    public Task DeleteAssignmentAsync(Assignment assignment)
    {
        _dbContext.Assignments.Remove(assignment);
        _dbContext.SaveChangesAsync();

        return Task.CompletedTask;
    }

    public async Task<bool> ExistsAsync(Guid assignmentId)
    {
        return await _dbContext.Assignments
            .AsNoTracking()
            .AnyAsync(assignment => assignment.Id == assignmentId);
    }

    public async Task<Assignment?> GetByIdAsync(Guid assignmentId)
    {
        return await _dbContext.Assignments.FirstOrDefaultAsync(assignment => assignment.Id == assignmentId);
    }

    public async Task<List<Assignment>> ListAssignmentAsync()
    {
        return await _dbContext.Assignments.ToListAsync();
    }

    public async Task UpdateAssignmentAsync(Assignment assignment)
    {
        _dbContext.Assignments.Update(assignment);
        await _dbContext.SaveChangesAsync();
    }
}
