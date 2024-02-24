using Logic.TechnicalAssement.Core.Commands;
using Logic.TechnicalAssement.Core.Entities;
using Logic.TechnicalAssement.Core.Queries;
using Microsoft.EntityFrameworkCore;

namespace Logic.TechnicalAssement.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext, IDbContext, IReadOnlyDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public Task<int> SaveChangesAsync() => base.SaveChangesAsync();

        public DbSet<Leave> LeaveRequests { get; set; }
    }
}
