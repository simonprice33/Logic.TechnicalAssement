using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Logic.TechnicalAssement.Core.Commands.DeleteLeaveCommand
{
    public class DeleteLeaveCommand : IRequestHandler<DeleteLeaveRequest, DeleteLeaveResponse>
    {
        private readonly IDbContext _dbContext;
        private readonly ILogger<DeleteLeaveCommand> _logger;

        public DeleteLeaveCommand(IDbContext dbContext, ILogger<DeleteLeaveCommand> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public async Task<DeleteLeaveResponse> Handle(DeleteLeaveRequest request, CancellationToken cancellationToken)
        {
            var leaveRequest = await _dbContext.LeaveRequests.FirstOrDefaultAsync(x => x.Id == request.Id).ConfigureAwait(false);

            if (leaveRequest == null)
            {
                // if entity doesn't exist, nothing to delete
                // return will be a 204 no content over a 404 not found
                return new DeleteLeaveResponse();
            }

            _dbContext.LeaveRequests.Remove(leaveRequest);
            await _dbContext.SaveChangesAsync();

            return new DeleteLeaveResponse();
        }
    }
}
