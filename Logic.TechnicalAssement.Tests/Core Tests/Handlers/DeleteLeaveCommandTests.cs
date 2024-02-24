using FluentAssertions;
using Logic.TechnicalAssement.Core.Commands.DeleteLeaveCommand;
using Logic.TechnicalAssement.Infrastructure.Data;
using Logic.TechnicalAssement.Tests.Helpers;
using Microsoft.Extensions.Logging;
using NSubstitute;

namespace Logic.TechnicalAssement.Tests.Core_Tests.Handlers
{
    public class DeleteLeaveCommandTests
    {
        private readonly ILogger<DeleteLeaveCommand> _logger;
        private readonly ApplicationDbContext _dbContext;

        public DeleteLeaveCommandTests()
        {
            var db = new InMemoryDatabase();
            _dbContext = db.Context;
            _logger = Substitute.For<ILogger<DeleteLeaveCommand>>();
        }

        [Theory]
        [InlineData(1)]
        [InlineData(100000)]
        public async Task Handle_GivenValidRequest_DeletesLeaveRequest(int id)
        {
            // Arrange
            var request = new DeleteLeaveRequest()
            {
                Id = id,
            };

            // Act
            var sut = CreateSut();
            var result = await sut.Handle(request, CancellationToken.None);

            // Assert
            result.Should().BeOfType<DeleteLeaveResponse>();
        }

        public DeleteLeaveCommand CreateSut()
        {
            return new DeleteLeaveCommand(_dbContext, _logger);
        }
    }
}
