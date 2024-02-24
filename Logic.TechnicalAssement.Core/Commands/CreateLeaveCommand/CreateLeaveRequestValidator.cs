using FluentValidation;

namespace Logic.TechnicalAssement.Core.Commands.CreateLeaveCommand
{
    public class CreateLeaveRequestValidator : AbstractValidator<CreateLeaveRequest>
    {
        public CreateLeaveRequestValidator()
        {
            RuleFor(request => request.FirstName).NotEmpty();
            RuleFor(request => request.LastName).NotEmpty();
            RuleFor(request => request.Email).NotEmpty().EmailAddress();
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
