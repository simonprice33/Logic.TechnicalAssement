using FluentValidation;
using Logic.TechnicalAssement.Core.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Logic.TechnicalAssement.Core.Commands.UpdateLeaveCommand
{
    public class UpdateLeaveCommand : IRequestHandler<UpdateLeaveRequest, UpdateLeaveResponse>
    {
        private readonly IDbContext _dbContext;
        private readonly ILogger<UpdateLeaveCommand> _logger;

        public UpdateLeaveCommand(IDbContext dbContext, ILogger<UpdateLeaveCommand> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public async Task<UpdateLeaveResponse> Handle(UpdateLeaveRequest request, CancellationToken cancellationToken)
        {
            var validator = new UpdateLeaveRequestValidator();
            await validator.ValidateAndThrowAsync(request, cancellationToken);

            var leaveRequest = await _dbContext.LeaveRequests
                .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken)
                .ConfigureAwait(false);

            if (leaveRequest == null)
            {
                return null;
            }

            UpdateDates(request, leaveRequest);

            await _dbContext.SaveChangesAsync();

            return new UpdateLeaveResponse();
        }

        private static void UpdateDates(UpdateLeaveRequest request, Leave leaveRequest)
        {
            if (leaveRequest.CanUpdateStartDate(request.StartDate))
            {
                leaveRequest.UpdatedStartDate(request.StartDate);
            }

            if (leaveRequest.CanUpdateEndDate(request.EndDate))
            {
                leaveRequest.UpdatedEndDate(request.EndDate);
            }

            if (leaveRequest.CanUpdateHalfDayFlag(request.IsHalfDay))
            {
                leaveRequest.UpdateHalfDayFlag(request.IsHalfDay);
            }

            if (leaveRequest.CanUpdateLeaveType(request.LeaveType))
            {
                leaveRequest.UpdateLeaveType(request.LeaveType);
            }
        }
    }
}
