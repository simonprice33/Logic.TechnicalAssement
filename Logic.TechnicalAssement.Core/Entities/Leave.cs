using Logic.TechnicalAssement.Core.Enums;

namespace Logic.TechnicalAssement.Core.Entities
{
    public class Leave
    {
        public Leave()
        {
            // Intentionally empty
        }

        public Leave(string email, string firstName, string lastName, DateTime leaveStart, DateTime leaveEnd, bool isHalfDay, LeaveType leaveType)
        {
            Email = email;
            FirstName = firstName;
            LastName = lastName;
            StartDate = leaveStart;
            EndDate = leaveEnd;
            IsHalfDay = isHalfDay;
            LeaveType = leaveType;
            CreatedDate = DateTime.Now;

        }

        public int Id { get; set; }

        public LeaveType LeaveType { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public bool IsHalfDay { get; set; }

        public DateTime CreatedDate { get; private set; }

        public DateTime UpdatedDate { get; private set; } = DateTime.Now;

        public string RequestActionedBy { get; private set; } = string.Empty;

        public bool CanUpdateStartDate(DateTime leaveStart)
        {
            return StartDate != leaveStart;
        }

        public void UpdatedStartDate(DateTime leaveStart)
        {
            StartDate = leaveStart;
            UpdatedDate = DateTime.Now;
        }

        public bool CanUpdateEndDate(DateTime leaveEnd)
        {
            return EndDate != leaveEnd;
        }

        public void UpdatedEndDate(DateTime leaveEnd)
        {
            EndDate = leaveEnd;
            UpdatedDate = DateTime.Now;
        }

        public bool CanUpdateHalfDayFlag(bool isHalfDay)
        {
            return IsHalfDay != isHalfDay;
        }

        public void UpdateHalfDayFlag(bool isHalfDay)
        {
            IsHalfDay = isHalfDay;
            UpdatedDate = DateTime.Now;
        }

        public bool CanUpdateLeaveType(LeaveType leaveType)
        {
            return LeaveType != leaveType;
        }

        public void UpdateLeaveType(LeaveType leaveType)
        {
            LeaveType = leaveType;
            UpdatedDate = DateTime.Now;
        }
    }
}
