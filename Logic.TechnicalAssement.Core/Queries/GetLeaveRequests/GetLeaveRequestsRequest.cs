using MediatR;

namespace Logic.TechnicalAssement.Core.Queries.GetLeaveRequests
{
    public class GetLeaveRequestsRequest : IRequest<GetLeaveRequestsResponse>
    {
        // intentionally blank as no query paramters to pass in
    }
}
