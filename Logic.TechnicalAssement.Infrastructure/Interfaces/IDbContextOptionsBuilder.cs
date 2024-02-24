using Logic.TechnicalAssement.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Logic.TechnicalAssement.Infrastructure.Interfaces
{
    public interface IDbContextOptionsBuilder
    {
        /// <summary>
        /// Build Database Options
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="configuration"></param>
        void BuildDbOptions(DbContextOptionsBuilder<ApplicationDbContext> builder, IConfiguration configuration);
    }
}