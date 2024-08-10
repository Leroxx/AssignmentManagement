using AssignmentManagement.Api.Domain.Assignment;
using AssignmentManagement.Api.Services.Common;
using ErrorOr;
using MediatR;

namespace AssignmentManagement.Api.Services.AssignmentsServices.Queries;

public record ListAssignmentsQuery() : IRequest<ErrorOr<List<Assignment>>>;

public class ListAssignmentsQueryHandler : IRequestHandler<ListAssignmentsQuery, ErrorOr<List<Assignment>>>
{
    private readonly IAssignmentRepository _assignmentRepository;

    public ListAssignmentsQueryHandler(IAssignmentRepository assignmentRepository)
    {
        _assignmentRepository = assignmentRepository;
    }

    public async Task<ErrorOr<List<Assignment>>> Handle(ListAssignmentsQuery query, CancellationToken cancellationToken)
    {
        var assignments = await _assignmentRepository.ListAssignmentAsync();
        return assignments.Count > 0 ? assignments : Error.NotFound(description: "There is no assignments to list");
    }
}