using AutoMapper;
using Logic.TechnicalAssement.Core.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Logic.TechnicalAssement.Core.Queries.GetLeaveRequests
{
    public class GetLeaveRequestsQuery : IRequestHandler<GetLeaveRequestsRequest, GetLeaveRequestsResponse>
    {
        private readonly IReadOnlyDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly ILogger<GetLeaveRequestsQuery> _logger;

        public GetLeaveRequestsQuery(
            IReadOnlyDbContext dbContext,
            IMapper mapper,
            ILogger<GetLeaveRequestsQuery> logger)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<GetLeaveRequestsResponse> Handle(GetLeaveRequestsRequest request, CancellationToken cancellationToken)
        {
            // Although I can get the objects directly from the database and put them in a list
            // having them in an IQueryable object allows me to use Automapper and project
            var query = _dbContext.LeaveRequests.AsQueryable();

            // Project here allows the SQL call to be refined only down to the object(s) that I need
            // Rather than a select * from ... this is now a select col1, col2, col3 
            var mappedResult = await _mapper.ProjectTo<LeaveViewModel>(query).ToListAsync(cancellationToken).ConfigureAwait(false);

            _logger.LogInformation("found {results} from LeaveRequest", mappedResult.Count);

            return new GetLeaveRequestsResponse
            {
                LeaveRequests = mappedResult
            };
        }
    }
}
