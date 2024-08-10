using AssignmentManagement.Api.Services.AssignmentsServices.Commands;
using AssignmentManagement.Api.Services.Common;

using ErrorOr;

using FluentAssertions;

using Moq;

namespace AssignmentManagement.Api.Test.AssignmentsUnitTests;

public class CreateAssignmentCommandHandlerTest
{
    private readonly Mock<IAssignmentRepository> _assignmentRepositoryMock;

    public CreateAssignmentCommandHandlerTest()
    {
        _assignmentRepositoryMock = new();
    }

    [Fact]
    public async Task Handle_Should_ReturnFailureResult_WhenDescriptionIsEmpty()
    {
        // Arrange
        var command = new CreateAssignmentCommand("Clean Home", "");
        var handler = new CreateAssignmentCommandHandler(_assignmentRepositoryMock.Object);

        // Act
        var result = await handler.Handle(command, default);

        // Assert
        result.IsError.Should().BeTrue();
        var error = result.Errors.First();
        error.Type.Should().Be(ErrorType.Validation);
        error.Code.Should().Be("Description");
    }

    [Fact]
    public async Task Handle_Should_ReturnFailureResult_WhitErrorNameLength()
    {
        // Arrange
        var command = new CreateAssignmentCommand("C", "Clean Home");
        var handler = new CreateAssignmentCommandHandler(_assignmentRepositoryMock.Object);

        // Act
        var result = await handler.Handle(command, default);

        // Assert
        result.IsError.Should().BeTrue();
        var error = result.Errors.First();
        error.Type.Should().Be(ErrorType.Validation);
        error.Code.Should().Be("Name");
    }

    [Fact]
    public async Task Handle_Should_ReturnFailureResult_WhitAllInvalidData()
    {
        // Arrange
        var command = new CreateAssignmentCommand("C", "Clean Home");
        var handler = new CreateAssignmentCommandHandler(_assignmentRepositoryMock.Object);

        // Act
        var result = await handler.Handle(command, default);

        // Assert
        result.IsError.Should().BeTrue();
        result.Errors
            .Select(error => error.Should().Be(error.Type == ErrorType.Validation));
    }

    [Fact]
    public async Task Handle_Should_ReturnValidResult()
    {
        // Arrange
        var command = new CreateAssignmentCommand("Clean", "Clean Home");
        var handler = new CreateAssignmentCommandHandler(_assignmentRepositoryMock.Object);

        // Act
        var result = await handler.Handle(command, default);

        // Assert
        result.IsError.Should().BeFalse();
        result.Value.Name.Should().Be("Clean");
    }
}
