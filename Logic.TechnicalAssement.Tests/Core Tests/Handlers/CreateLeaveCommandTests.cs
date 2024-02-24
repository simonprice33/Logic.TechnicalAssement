using FluentAssertions;
using FluentValidation;
using Logic.TechnicalAssement.Core.Commands;
using Logic.TechnicalAssement.Core.Commands.CreateLeaveCommand;
using Logic.TechnicalAssement.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NSubstitute;

namespace Logic.TechnicalAssement.Tests.Core_Tests.Handlers
{
    public class CreateLeaveCommandTests
    {
        private readonly ILogger<CreateLeaveCommand> _logger;
        private readonly IDbContext _dbContext;
        private readonly List<Leave> _leaveRequests = new List<Leave>();

        public CreateLeaveCommandTests()
        {
            _logger = Substitute.For<ILogger<CreateLeaveCommand>>();
            _dbContext = Substitute.For<IDbContext>();

            var dbSetMock = Substitute.For<DbSet<Leave>, IQueryable<Leave>>();
            ((IQueryable<Leave>)dbSetMock).Provider.Returns(_leaveRequests.AsQueryable().Provider);
            ((IQueryable<Leave>)dbSetMock).Expression.Returns(_leaveRequests.AsQueryable().Expression);
            ((IQueryable<Leave>)dbSetMock).ElementType.Returns(_leaveRequests.AsQueryable().ElementType);
            ((IQueryable<Leave>)dbSetMock).GetEnumerator().Returns(_leaveRequests.GetEnumerator());

            dbSetMock.When(x => x.Add(Arg.Any<Leave>())).Do(callInfo =>
            {
                var entity = callInfo.Arg<Leave>();
                _leaveRequests.Add(entity);
                entity.Id = _leaveRequests.Count;
            });

            _dbContext.LeaveRequests.Returns(dbSetMock);
        }

        [Fact]
        public async Task Handle_ValidRequest_ShouldAddLeaveRequest()
        {
            // Arrange
            var request = new CreateLeaveRequest
            {
                Email = "test@example.com",
                FirstName = "Test",
                LastName = "User",
                StartDate = DateTime.Today,
                EndDate = DateTime.Today.AddDays(1),
                IsHalfDay = false
            };

            // Act
            var sut = CreateSut();
            var result = await sut.Handle(request, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.Id.Should().BeGreaterThan(0);
            await _dbContext.Received(1).SaveChangesAsync();
        }

        [Fact]
        public async Task Handle_InvalidRequest_ShouldThrowValidationException()
        {
            // Arrange
            var request = new CreateLeaveRequest
            {
                Email = "",
                FirstName = "",
                LastName = "",
                StartDate = DateTime.Today,
                EndDate = DateTime.Today.AddDays(-1), // Assuming a negative date range is invalid
                IsHalfDay = false
            };

            // Act & Assert
            var sut = CreateSut();
            await Assert.ThrowsAsync<ValidationException>(() => sut.Handle(request, CancellationToken.None));
        }

        private CreateLeaveCommand CreateSut()
        {
            return new CreateLeaveCommand(_dbContext, _logger);
        }
    }
}
