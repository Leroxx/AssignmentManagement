using AssignmentManagement.Api.Domain.Assignment;
using AssignmentManagement.Api.Services.Common;

using ErrorOr;

using FluentValidation;

using MediatR;

namespace AssignmentManagement.Api.Services.AssignmentsServices.Commands;

public record UpdateAssignmentCommand(Guid AssignmentId, string Name, string Description) : IRequest<ErrorOr<Assignment>>;

public class UpdateAssignmentCommandHandler : IRequestHandler<UpdateAssignmentCommand, ErrorOr<Assignment>>
{
    private readonly IAssignmentRepository _assignmentRepository;

    public UpdateAssignmentCommandHandler(IAssignmentRepository assignmentRepository)
    {
        _assignmentRepository = assignmentRepository;
    }

    public async Task<ErrorOr<Assignment>> Handle(UpdateAssignmentCommand command, CancellationToken cancellationToken)
    {
        var validator = new UpdateAssignmentCommandValidator();
        var validationResult = await validator.ValidateAsync(command);

        if (!validationResult.IsValid)
        {
            return validationResult.Errors
                .Select(error => Error.Validation(code: error.PropertyName, description: error.ErrorMessage))
                .ToList();
        }

        var assignment = await _assignmentRepository.GetByIdAsync(command.AssignmentId);

        if (assignment is null)
        {
            return Error.NotFound(description: "Assignment not found");
        }

        assignment.Update(command.Name, command.Description);

        await _assignmentRepository.UpdateAssignmentAsync(assignment);

        return assignment;
    }
}


public class UpdateAssignmentCommandValidator : AbstractValidator<UpdateAssignmentCommand>
{
    public UpdateAssignmentCommandValidator()
    {
        RuleFor(x => x.Name)
            .MinimumLength(3)
            .MaximumLength(150);

        RuleFor(x => x.Description)
            .NotEmpty();
    }
}
