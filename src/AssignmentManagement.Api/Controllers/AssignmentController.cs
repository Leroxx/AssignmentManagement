using AssignmentManagement.Contracts.Assignments;
using DomainAssignmentStateType = AssignmentManagement.Api.Domain.Assignments.AssignmentStateType;
using Microsoft.AspNetCore.Mvc;
using AssignmentManagement.Api.Services.AssignmentsServices.Queries;
using MediatR;
using AssignmentManagement.Api.Services.AssignmentsServices.Commands;
using AssignmentManagement.Api.Domain.Assignment;

namespace AssignmentManagement.Api.Controllers;

[Route("[controller]")]
[Consumes("application/json")]
[Produces("application/json")]
public class AssignmentsController : ApiController
{
    private readonly ISender _mediator;

    public AssignmentsController(ISender mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Create a new Assignment
    /// </summary>
    /// <remarks>
    /// Sample request:
    /// 
    ///     POST /assignments
    ///     {        
    ///       "name": "Cook",
    ///       "description": "Cook dinner"
    ///     }
    /// </remarks>
    /// <param name="request">AssignmentRequest</param>
    [HttpPost]
    [ProducesResponseType(typeof(Assignment), StatusCodes.Status201Created)]
    [ProducesResponseType(statusCode:StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateAssignment(CreateAssignmentRequest request)
    {
        var command = new CreateAssignmentCommand(request.Name, request.Description);

        var createAssignmentResult = await _mediator.Send(command);

        return createAssignmentResult.Match(
            assignment => CreatedAtAction(
                nameof(GetAssignment),
                new { assignmentId = assignment.Id },
                new AssignmentResponse(
                    assignment.Id,
                    assignment.Name,
                    assignment.Description,
                    ToDto(assignment.State),
                    assignment.CreatedDateTime,
                    assignment.LastModifiedDateTime)),
            Problem);
    }

    /// <summary>
    /// Get an Assignment
    /// </summary>
    /// <remarks>
    /// Sample request:
    ///     "id":"a5fa1e11-5fd9-4ccd-8825-25a5ef786ace"
    /// </remarks>
    /// <param name="assignmentId">AssignmentRequest</param>
    [HttpGet("{assignmentId:guid}")]
    [ProducesResponseType(typeof(Assignment), StatusCodes.Status200OK)]
    [ProducesResponseType(statusCode:StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAssignment(Guid assignmentId)
    {
        var command = new GetAssignment(assignmentId);

        var getAssignmentResult = await _mediator.Send(command);

        return getAssignmentResult.MatchFirst(
            assignment => Ok(new AssignmentResponse(
                assignment.Id,
                assignment.Name,
                assignment.Description,
                ToDto(assignment.State),
                assignment.CreatedDateTime,
                assignment.LastModifiedDateTime)),
            Problem);
    }

    /// <summary>
    /// List all Assignments
    /// </summary>
    [HttpGet]
    [ProducesResponseType(typeof(Assignment), StatusCodes.Status200OK)]
    [ProducesResponseType(statusCode:StatusCodes.Status404NotFound)]
    public async Task<IActionResult> ListAssignments()
    {
        var command = new ListAssignmentsQuery();

        var listAssignments = await _mediator.Send(command);

        return listAssignments.Match(
            assignments => Ok(assignments.ConvertAll(assignment => new AssignmentResponse(
                assignment.Id,
                assignment.Name,
                assignment.Description,
                ToDto(assignment.State),
                assignment.CreatedDateTime,
                assignment.LastModifiedDateTime))),
            Problem);
    }

    /// <summary>
    /// Update an Assignment
    /// </summary>
    /// <remarks>
    /// Sample request:
    /// 
    ///     PUT /assignments/a5fa1e11-5fd9-4ccd-8825-25a5ef786ace
    ///     {        
    ///       "name": "Clean",
    ///       "description": "Clean kitchen"
    ///     }
    /// </remarks>
    /// <param name="assignmentId"></param>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPut("{assignmentId:guid}")]
    [ProducesResponseType(typeof(Assignment), StatusCodes.Status204NoContent)]
    [ProducesResponseType(statusCode:StatusCodes.Status400BadRequest)]
    [ProducesResponseType(statusCode:StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateAssignment(Guid assignmentId, UpdateAssignmentRequest request)
    {
        var command = new UpdateAssignmentCommand(
            assignmentId,
            request.Name,
            request.Description);

        var updateAssignmentResult = await _mediator.Send(command);

        return updateAssignmentResult.Match(
            _ => NoContent(),
            Problem);
    }


    /// <summary>
    /// Complete an Assignment
    /// </summary>
    /// <remarks>
    /// Sample request:
    ///     PUT /assignments/complete/a5fa1e11-5fd9-4ccd-8825-25a5ef786ace
    ///</remarks>
    /// <param name="assignmentId"></param>
    /// <returns></returns>
    [HttpPut("complete/{assignmentId:guid}")]
    [ProducesResponseType(typeof(Assignment), StatusCodes.Status204NoContent)]
    [ProducesResponseType(statusCode:StatusCodes.Status400BadRequest)]
    [ProducesResponseType(statusCode:StatusCodes.Status404NotFound)]

    public async Task<IActionResult> CompleteAssignment(Guid assignmentId)
    {
        var command = new CompleteAssignmentCommand(assignmentId);

        var updateAssignmentResult = await _mediator.Send(command);

        return updateAssignmentResult.Match(
            _ => NoContent(),
            Problem);
    }

    /// <summary>
    /// Complete an Assignment
    /// </summary>
    /// <remarks>
    /// Sample request:
    ///     DELETE /assignments/a5fa1e11-5fd9-4ccd-8825-25a5ef786ace
    ///</remarks>
    /// <param name="assignmentId"></param>
    /// <returns></returns>
    [HttpDelete("{assignmentId:guid}")]
    [ProducesResponseType(typeof(Assignment), StatusCodes.Status204NoContent)]
    [ProducesResponseType(statusCode:StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteAssignment(Guid assignmentId)
    {
        var command = new DeleteAssignmentCommand(assignmentId);

        var deleteAssignmentResult = await _mediator.Send(command);

        return deleteAssignmentResult.Match(
            _ => NoContent(),
            Problem);
    }

    private static AssignmentStateType ToDto(DomainAssignmentStateType assignmentStateType)
    {
        return assignmentStateType.Name switch
        {
            nameof(DomainAssignmentStateType.InProgress) => AssignmentStateType.InProgress,
            nameof(DomainAssignmentStateType.Completed) => AssignmentStateType.Completed,
            _ => throw new InvalidOperationException(),
        };
    }
}
