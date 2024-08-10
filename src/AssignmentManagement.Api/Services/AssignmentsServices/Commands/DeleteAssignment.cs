namespace AssignmentManagement.Api.Services.AssignmentsServices.Commands;

using AssignmentManagement.Api.Domain.Assignment;
using AssignmentManagement.Api.Services.Common;

using ErrorOr;
using MediatR;

public record DeleteAssignmentCommand(Guid AssignmentId) : IRequest<ErrorOr<Deleted>>;

public class DeleteAssignmentCommandHandler : IRequestHandler<DeleteAssignmentCommand, ErrorOr<Deleted>>
{
    private readonly IAssignmentRepository _assignmentRepository;

    public DeleteAssignmentCommandHandler(IAssignmentRepository assignmentRepository)
    {
        _assignmentRepository = assignmentRepository;
    }

    public async Task<ErrorOr<Deleted>> Handle(DeleteAssignmentCommand command, CancellationToken cancellationToken)
    {
        var assignment = await _assignmentRepository.GetByIdAsync(command.AssignmentId);

        if (assignment is null)
        {
            return Error.NotFound(description: "Assignment not found");
        }

        await _assignmentRepository.DeleteAssignmentAsync(assignment);

        return Result.Deleted;
    }
}