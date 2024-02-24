using FluentAssertions;
using FluentValidation;
using FluentValidation.Results;
using Logic.TechnicalAssement.App.Controllers;
using Logic.TechnicalAssement.Core.Commands.CreateLeaveCommand;
using Logic.TechnicalAssement.Core.Commands.UpdateLeaveCommand;
using Logic.TechnicalAssement.Core.Queries.GetLeaveRequests;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NSubstitute;

namespace Logic.TechnicalAssement.Tests.App_Tests
{
    public class LeaveControllerTests
    {
        private readonly ILogger<LeaveController> _logger;
        private readonly IMediator _mediator;

        public LeaveControllerTests()
        {
            _logger = Substitute.For<ILogger<LeaveController>>();
            _mediator = Substitute.For<IMediator>();
        }

        [Fact]
        public async Task Index_ShouldReturnViewResult_WithLeaveRequests()
        {
            // Arrange
            var mockResult = new GetLeaveRequestsResponse();
            _mediator.Send(Arg.Any<GetLeaveRequestsRequest>(), Arg.Any<CancellationToken>()).Returns(mockResult);

            // Act
            var sut = CreateSut();
            var result = await sut.Index();

            // Assert
            result.Should().BeOfType<ViewResult>();
            var viewResult = result as ViewResult;
            viewResult.Model.Should().BeEquivalentTo(mockResult);
        }

        [Fact]
        public async Task CreateLeaveRequest_WithValidRequest_ShouldReturnCreatedAtActionResult()
        {
            // Arrange
            var request = new CreateLeaveRequest()
            ;
            var mockResult = new CreateLeaveResponse() { Id = 1 };
            _mediator.Send(Arg.Any<CreateLeaveRequest>(), Arg.Any<CancellationToken>()).Returns(mockResult);

            // Act
            var sut = CreateSut();
            var result = await sut.CreateLeaveRequest(request);

            // Assert
            result.Should().BeOfType<CreatedAtActionResult>();
        }

        [Fact]
        public async Task CreateLeaveRequest_WithValidationException_ShouldReturnBadRequest()
        {
            // Arrange
            var request = new CreateLeaveRequest(); // Setup your request with data that would fail validation
            var validationErrors = new List<ValidationFailure> {
                new ValidationFailure("Field", "Error Message")
            };
            _mediator.Send(Arg.Any<CreateLeaveRequest>(), Arg.Any<CancellationToken>())
                .Returns(Task.FromException<CreateLeaveResponse>(new ValidationException(validationErrors)));

            // Act
            var sut = CreateSut();
            var result = await sut.CreateLeaveRequest(request);

            // Assert
            result.Should().BeOfType<BadRequestObjectResult>();
            var badRequestResult = result as BadRequestObjectResult;
            badRequestResult.Value.Should().BeEquivalentTo(validationErrors);
        }

        [Fact]
        public async Task CreateLeaveRequest_WithGenericException_ShouldRethrowException()
        {
            // Arrange
            var request = new CreateLeaveRequest(); // Setup your request
            var exception = new Exception("Unexpected error");
            _mediator.Send(Arg.Any<CreateLeaveRequest>(), Arg.Any<CancellationToken>())
                .Returns(Task.FromException<CreateLeaveResponse>(exception));

            // Act & Assert
            var sut = CreateSut();
            var act = () => sut.CreateLeaveRequest(request);

            // Assert that the async method throws the exact exception
            await act.Should().ThrowAsync<Exception>().WithMessage("Unexpected error");

            // Optionally, verify that the error was logged
            _logger.Received(1).LogError(exception, exception.Message);
        }

        [Fact]
        public async Task UpdateLeaveRequest_WithValidRequest_ShouldReturnNotFoundResult()
        {
            // Arrange
            var request = new UpdateLeaveRequest();
            _mediator.Send(Arg.Any<UpdateLeaveRequest>(), Arg.Any<CancellationToken>()).Returns(Task.FromResult<UpdateLeaveResponse>(null!));

            // Act
            var sut = CreateSut();
            var result = await sut.UpdateLeaveRequest(request);

            // Assert
            result.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public async Task UpdateLeaveRequest_WithValidRequest_ShouldReturnNoContentResult()
        {
            // Arrange
            var request = new UpdateLeaveRequest();
            _mediator.Send(Arg.Any<UpdateLeaveRequest>(), Arg.Any<CancellationToken>()).Returns(Task.FromResult(new UpdateLeaveResponse()));

            // Act
            var sut = CreateSut();
            var result = await sut.UpdateLeaveRequest(request);

            // Assert
            result.Should().BeOfType<NoContentResult>();
        }

        [Fact]
        public async Task DeleteLeaveRequest_WithValidId_ShouldReturnNoContentResult()
        {
            // Act
            var sut = CreateSut();
            var result = await sut.DeleteLEaveRequest(1);

            // Assert
            result.Should().BeOfType<NoContentResult>();
        }

        private LeaveController CreateSut()
        {
            return new LeaveController(_logger, _mediator);
        }
    }
}
