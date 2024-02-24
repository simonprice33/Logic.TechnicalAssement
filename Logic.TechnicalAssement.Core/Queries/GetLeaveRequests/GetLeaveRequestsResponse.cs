using Logic.TechnicalAssement.Core.Models;

namespace Logic.TechnicalAssement.Core.Queries.GetLeaveRequests
{
    public class GetLeaveRequestsResponse
    {
        public GetLeaveRequestsResponse()
        {
            LeaveRequests = new List<LeaveViewModel>();
        }
        public List<LeaveViewModel> LeaveRequests { get; set; }
    }
}
