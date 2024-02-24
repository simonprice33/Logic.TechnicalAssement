using Logic.TechnicalAssement.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Logic.TechnicalAssement.Core.Commands
{
    public interface IDbContext
    {
        DbSet<Leave> LeaveRequests { get; set; }

        public Task<int> SaveChangesAsync();
    }
}
