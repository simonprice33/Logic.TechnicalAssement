using FluentAssertions;
using Logic.TechnicalAssement.Core.Commands.UpdateLeaveCommand;
using Logic.TechnicalAssement.Core.Enums;
using Logic.TechnicalAssement.Infrastructure.Data;
using Logic.TechnicalAssement.Tests.Helpers;
using Microsoft.Extensions.Logging;
using NSubstitute;

namespace Logic.TechnicalAssement.Tests.Core_Tests.Handlers
{
    public class UpateLeaveCommandTests
    {
        private readonly ILogger<UpdateLeaveCommand> _logger;
        private readonly ApplicationDbContext _dbContext;

        public UpateLeaveCommandTests()
        {
            var db = new InMemoryDatabase();
            _dbContext = db.Context;
            _logger = Substitute.For<ILogger<UpdateLeaveCommand>>();
        }

        [Theory]
        [InlineData(-14, 2, false, LeaveType.UnpaidLeave)]
        [InlineData(-14, 2, false, LeaveType.SickLeave)]
        [InlineData(2, 2, true, LeaveType.SickLeave)]
        public async Task Handle_GivenValidRequest_UpdatesLeaveRequest(int startDays, int endDays, bool isHalfDay, LeaveType leaveType)
        {
            // Arrange
            var request = new UpdateLeaveRequest
            {
                Id = 1,
                StartDate = DateTime.Now.AddDays(startDays),
                EndDate = DateTime.Now.AddDays(endDays),
                IsHalfDay = isHalfDay,
                LeaveType = leaveType
            };

            // Act
            var sut = CreateSut();
            var result = await sut.Handle(request, CancellationToken.None);

            // Assert
            result.Should().BeOfType<UpdateLeaveResponse>();
        }

        [Fact]
        public async Task Handle_GivenInvalidId_ReturnsNull()
        {
            // Arrange
            var request = new UpdateLeaveRequest { Id = 99 }; // Assuming a non-existing ID

            // Act
            var sut = CreateSut();
            var result = await sut.Handle(request, CancellationToken.None);

            // Assert
            result.Should().BeNull();
        }

        private UpdateLeaveCommand CreateSut()
        {
            return new UpdateLeaveCommand(_dbContext, _logger);
        }
    }
}
