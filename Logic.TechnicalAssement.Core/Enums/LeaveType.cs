using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Logic.TechnicalAssement.Core.Enums
{
    public enum LeaveType
    {
        [Display(Name = "Annual Leave")]
        [Description("Annual Leave")]
        AnnualLeave,
        [Display(Name = "Sick Leave")]
        [Description("Sick Leave")]
        SickLeave,
        [Display(Name = "Compassionate Leave")]
        [Description("Compassionate Leave")]
        CompassionateLeave,
        [Display(Name = "Unpaid Leave")]
        [Description("Unpaid Leave")]
        UnpaidLeave,
    }
}
