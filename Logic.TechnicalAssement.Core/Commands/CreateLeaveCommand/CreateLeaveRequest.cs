using Logic.TechnicalAssement.Core.Enums;
using MediatR;

namespace Logic.TechnicalAssement.Core.Commands.CreateLeaveCommand
{
    public class CreateLeaveRequest : IRequest<CreateLeaveResponse>
    {
        public LeaveType LeaveType { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public bool IsHalfDay { get; set; }
    }
}
