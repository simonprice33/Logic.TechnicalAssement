using FluentValidation;
using Logic.TechnicalAssement.Core.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Logic.TechnicalAssement.Core.Commands.CreateLeaveCommand
{
    public class CreateLeaveCommand : IRequestHandler<CreateLeaveRequest, CreateLeaveResponse>
    {
        private readonly IDbContext _dbContext;
        private readonly ILogger<CreateLeaveCommand> _logger;

        public CreateLeaveCommand(IDbContext dbContext, ILogger<CreateLeaveCommand> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public async Task<CreateLeaveResponse> Handle(CreateLeaveRequest request, CancellationToken cancellationToken)
        {
            var validator = new CreateLeaveRequestValidator();

            // No need to wrap this in a try catch block as this will throw the exception 
            // which will bubble up to the controller. 
            await validator.ValidateAndThrowAsync(request, cancellationToken);

            // As the validate and throw method asserts out model is correct we can continue
            var leaveRequest = new Leave(request.Email, request.FirstName, request.LastName, request.StartDate, request.EndDate, request.IsHalfDay, request.LeaveType);

            _dbContext.LeaveRequests.Add(leaveRequest);
            await _dbContext.SaveChangesAsync();

            return new CreateLeaveResponse
            {
                Id = leaveRequest.Id
            };
        }
    }
}
