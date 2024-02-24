using MediatR;

namespace Logic.TechnicalAssement.Core.Commands.DeleteLeaveCommand
{
    public class DeleteLeaveRequest : IRequest<DeleteLeaveResponse>
    {
        /// <summary>
        /// Gets or Sets Id
        /// </summary>
        public int Id { get; set; }
    }
}
