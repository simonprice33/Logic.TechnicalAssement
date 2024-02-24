using Logic.TechnicalAssement.Core.Entities;
using Logic.TechnicalAssement.Core.Enums;
using Logic.TechnicalAssement.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Logic.TechnicalAssement.Tests.Helpers
{
    public class InMemoryDatabase
    {
        public readonly ApplicationDbContext Context;

        public InMemoryDatabase()
        {
            // Setup in-memory database context
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: $"InMemoryAppDb-{Guid.NewGuid()}")
                .Options;

            Context = new ApplicationDbContext(options);

            Context.Database.EnsureCreated();

            SeedDatabase();
        }

        private void SeedDatabase()
        {
            // Add entities to the context as needed
            Context.LeaveRequests.Add(new Leave("test@test.com", "test", "user", DateTime.Now, DateTime.Now, false, LeaveType.AnnualLeave));
            Context.LeaveRequests.Add(new Leave("test@test.com", "test", "user", DateTime.Now.AddDays(-14), DateTime.Now, false, LeaveType.SickLeave));
            Context.SaveChanges();
        }
    }
}
