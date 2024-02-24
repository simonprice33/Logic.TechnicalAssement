using AutoMapper;
using FluentAssertions;
using Logic.TechnicalAssement.Core.MappingProfiles;
using Logic.TechnicalAssement.Core.Queries.GetLeaveRequests;
using Logic.TechnicalAssement.Infrastructure.Data;
using Logic.TechnicalAssement.Tests.Helpers;
using Microsoft.Extensions.Logging;
using NSubstitute;

namespace Logic.TechnicalAssement.Tests.Core_Tests.Handlers
{
    public class GetLeaveQueryTests
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly ILogger<GetLeaveRequestsQuery> _logger;
        private readonly Mapper _mapper;

        public GetLeaveQueryTests()
        {
            var db = new InMemoryDatabase();
            _dbContext = db.Context;
            _logger = Substitute.For<ILogger<GetLeaveRequestsQuery>>();

            _mapper = new Mapper(new MapperConfiguration(expression =>
                expression.AddProfile(new LeaveRequestProfile())
            ));
        }

        [Fact]
        public async Task GetLeaveRequests_Should_Return_Expected_Results()
        {
            //Act
            var sut = CreateSut();
            var result = await sut.Handle(new GetLeaveRequestsRequest(), CancellationToken.None);

            // Assert
            result.LeaveRequests.Should().HaveCount(2);
        }

        [Fact]
        public async Task GetLeaveRequests_Should_Return_Zero_Results()
        {
            // Arrange
            var records = _dbContext.LeaveRequests.ToList();
            _dbContext.RemoveRange(records);
            await _dbContext.SaveChangesAsync();

            // Act
            var sut = CreateSut();
            var result = await sut.Handle(new GetLeaveRequestsRequest(), CancellationToken.None);

            // Assert
            result.LeaveRequests.Should().HaveCount(0);
        }

        private GetLeaveRequestsQuery CreateSut()
        {
            return new GetLeaveRequestsQuery(_dbContext, _mapper, _logger);
        }
    }
}
