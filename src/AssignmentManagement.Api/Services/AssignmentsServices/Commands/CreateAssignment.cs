using AssignmentManagement.Api.Domain.Assignment;
using AssignmentManagement.Api.Services.Common;
using ErrorOr;
using FluentValidation;


using MediatR;

namespace AssignmentManagement.Api.Services.AssignmentsServices.Commands;

public record CreateAssignmentCommand(string Name, string Description) : IRequest<ErrorOr<Assignment>>;

public class CreateAssignmentCommandHandler : IRequestHandler<CreateAssignmentCommand, ErrorOr<Assignment>>
{
    private readonly IAssignmentRepository _assignmentRepository;

    public CreateAssignmentCommandHandler(IAssignmentRepository assignmentRepository)
    {
        _assignmentRepository = assignmentRepository;
    }

    public async Task<ErrorOr<Assignment>> Handle(CreateAssignmentCommand command, CancellationToken cancellationToken)
    {
        var validator = new CreateAssignmentCommandValidator();
        var validationResult = await validator.ValidateAsync(command);

        if (!validationResult.IsValid)
        {
            return validationResult.Errors
                .Select(error => Error.Validation(code: error.PropertyName, description: error.ErrorMessage))
                .ToList();
        }

        var assignment = new Assignment(
            command.Name,
            command.Description);

        await _assignmentRepository.AddAssignmentAsync(assignment);

        return assignment;
    }
}

public class CreateAssignmentCommandValidator : AbstractValidator<CreateAssignmentCommand>
{
    public CreateAssignmentCommandValidator()
    {
        RuleFor(x => x.Name)
            .MinimumLength(3)
            .MaximumLength(150);

        RuleFor(x => x.Description)
            .NotEmpty();
    }
}
