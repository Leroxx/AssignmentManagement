

using AssignmentManagement.Api.Domain.Assignment;
using AssignmentManagement.Api.Services.Common;

using ErrorOr;
using MediatR;

namespace AssignmentManagement.Api.Services.AssignmentsServices.Commands;

public record CompleteAssignmentCommand(Guid AssignmentId) : IRequest<ErrorOr<Success>>;

public class CompleteAssignmentCommandHandler : IRequestHandler<CompleteAssignmentCommand, ErrorOr<Success>>
{
    private readonly IAssignmentRepository _assignmentRepository;

    public CompleteAssignmentCommandHandler(IAssignmentRepository assignmentRepository)
    {
        _assignmentRepository = assignmentRepository;
    }

    public async Task<ErrorOr<Success>> Handle(CompleteAssignmentCommand command, CancellationToken cancellationToken)
    {
        Assignment? assignment = await _assignmentRepository.GetByIdAsync(command.AssignmentId);

        if (assignment is null)
        {
            return Error.NotFound(description: "Assignment not found");
        }

        var completeAssignmentResult = assignment.Complete();

        if (completeAssignmentResult.IsError)
        {
            return completeAssignmentResult.Errors;
        }

        await _assignmentRepository.UpdateAssignmentAsync(assignment);

        return Result.Success;
    }
}