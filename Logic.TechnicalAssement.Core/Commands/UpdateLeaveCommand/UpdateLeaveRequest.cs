using Logic.TechnicalAssement.Core.Enums;
using MediatR;

namespace Logic.TechnicalAssement.Core.Commands.UpdateLeaveCommand
{
    public class UpdateLeaveRequest : IRequest<UpdateLeaveResponse>
    {
        /// <summary>
        /// Gets or Sets Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or Sets Leave Start Date
        /// </summary>
        public DateTime StartDate { get; set; }

        /// <summary>
        /// Gets or Sets End Date
        /// </summary>
        public DateTime EndDate { get; set; }

        /// <summary>
        /// Gets or Sets Half Day Flag
        /// </summary>
        public bool IsHalfDay { get; set; }

        /// <summary>
        /// Gets or Sets Leave Type
        /// </summary>
        public LeaveType LeaveType { get; set; }
    }
}
