using Logic.TechnicalAssement.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Logic.TechnicalAssement.Core.Queries
{
    public interface IReadOnlyDbContext
    {
        DbSet<Leave> LeaveRequests { get; }

    }
}
