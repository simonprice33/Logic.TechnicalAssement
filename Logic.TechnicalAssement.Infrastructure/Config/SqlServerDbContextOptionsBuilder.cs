using Logic.TechnicalAssement.Infrastructure.Data;
using Logic.TechnicalAssement.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Diagnostics.CodeAnalysis;

namespace Logic.TechnicalAssement.Infrastructure.Config
{
    [ExcludeFromCodeCoverage]
    public class SqlServerDbContextOptionsBuilder : IDbContextOptionsBuilder
    {
        public void BuildDbOptions(DbContextOptionsBuilder<ApplicationDbContext> builder, IConfiguration configuration)
        {
            var dbPath = "app.db";

            builder
                .UseSqlite($"Data Source={dbPath}");
        }
    }
}