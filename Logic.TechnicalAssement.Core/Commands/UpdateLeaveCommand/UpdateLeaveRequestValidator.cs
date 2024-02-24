using FluentValidation;
using Logic.TechnicalAssement.Core.Queries;

namespace Logic.TechnicalAssement.Core.Commands.UpdateLeaveCommand
{
    public class UpdateLeaveRequestValidator : AbstractValidator<UpdateLeaveRequest>
    {
        private readonly IReadOnlyDbContext _dbContext;

        public UpdateLeaveRequestValidator()
        {
            RuleFor(request => request.LeaveType).IsInEnum();
            RuleFor(request => request.EndDate).GreaterThanOrEqualTo(request => request.StartDate);
            When(x => x.IsHalfDay, () =>
            {
                RuleFor(x => x.StartDate.Date)
                    .Equal(x => x.EndDate.Date)
                    .WithMessage("For a half-day leave, the start and end date must be the same.");
            });
        }
    }
}
