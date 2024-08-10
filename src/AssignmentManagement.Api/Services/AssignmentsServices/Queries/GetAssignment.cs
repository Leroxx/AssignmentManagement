using AssignmentManagement.Api.Domain.Assignment;
using AssignmentManagement.Api.Services.Common;
using ErrorOr;
using MediatR;

namespace AssignmentManagement.Api.Services.AssignmentsServices.Queries;

public record GetAssignment(Guid AssignmentId) : IRequest<ErrorOr<Assignment>>;

public class GetAssignmentQueryHandler : IRequestHandler<GetAssignment, ErrorOr<Assignment>>
{
    private readonly IAssignmentRepository _assignmentRepository;

    public GetAssignmentQueryHandler(IAssignmentRepository assignmentRepository)
    {
        _assignmentRepository = assignmentRepository;
    }

    public async Task<ErrorOr<Assignment>> Handle(GetAssignment query, CancellationToken cancellationToken)
    {
        if (await _assignmentRepository.GetByIdAsync(query.AssignmentId) is not Assignment assignment)
        {
            return Error.NotFound(description: "Assignment not found");
        }

        return assignment;
    }
}   
