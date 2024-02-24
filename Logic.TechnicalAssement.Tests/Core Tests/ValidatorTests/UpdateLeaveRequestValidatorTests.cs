using FluentValidation.TestHelper;
using Logic.TechnicalAssement.Core.Commands.UpdateLeaveCommand;
using Logic.TechnicalAssement.Core.Enums;

namespace Logic.TechnicalAssement.Tests.Core_Tests.ValidatorTests
{
    public class UpdateLeaveRequestValidatorTests
    {
        private readonly UpdateLeaveRequestValidator _validator = new UpdateLeaveRequestValidator();
        private readonly UpdateLeaveRequest _validRequest;

        public UpdateLeaveRequestValidatorTests()
        {
            _validRequest = new UpdateLeaveRequest()
            {
                EndDate = DateTime.Now,
                StartDate = DateTime.Now,
                IsHalfDay = false,
                LeaveType = LeaveType.AnnualLeave
            };
        }

        [Fact]
        public void Should_Validate_LeaveType_Is_InEnum()
        {
            _validRequest.LeaveType = (LeaveType)99;
            var validationResult = _validator.TestValidate(_validRequest);
            validationResult.ShouldHaveValidationErrorFor(request => request.LeaveType);
        }

        [Fact]
        public void EndDate_Should_Not_Be_Before_StartDate()
        {
            _validRequest.StartDate = DateTime.Now;
            _validRequest.EndDate = DateTime.Now.AddDays(-1);
            var validationResult = _validator.TestValidate(_validRequest);
            validationResult.ShouldHaveValidationErrorFor(request => request.EndDate);
        }

        [Fact]
        public void For_HalfDay_Leave_Start_And_End_Date_Should_Be_Same()
        {
            ;
            _validRequest.IsHalfDay = true;
            _validRequest.StartDate = DateTime.Now;
            _validRequest.EndDate = DateTime.Now.AddDays(1);
            var validationResult = _validator.TestValidate(_validRequest);
            validationResult.ShouldNotHaveValidationErrorFor(request => request.EndDate);
        }

        [Fact]
        public void For_HalfDay_Leave_Validation_Should_Pass_If_Start_And_End_Date_Are_Same()
        {
            var date = DateTime.Now;
            _validRequest.StartDate = date;
            _validRequest.EndDate = date;
            _validRequest.IsHalfDay = true;
            var validationResult = _validator.TestValidate(_validRequest);
            validationResult.ShouldNotHaveValidationErrorFor(request => request.EndDate);
        }
    }
}
