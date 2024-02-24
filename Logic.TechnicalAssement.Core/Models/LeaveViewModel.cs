using Logic.TechnicalAssement.Core.Enums;

namespace Logic.TechnicalAssement.Core.Models
{
    public class LeaveViewModel
    {
        public int Id { get; set; }

        public LeaveType LeaveType { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public bool IsHalfDay { get; set; }
    }
}
