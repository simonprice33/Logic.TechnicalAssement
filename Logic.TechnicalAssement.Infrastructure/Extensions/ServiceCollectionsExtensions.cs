using Logic.TechnicalAssement.Core.Commands;
using Logic.TechnicalAssement.Core.Queries;
using Logic.TechnicalAssement.Infrastructure.Config;
using Logic.TechnicalAssement.Infrastructure.Data;
using Logic.TechnicalAssement.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Logic.TechnicalAssement.Infrastructure.Extensions
{
    public static class ServiceCollectionsExtensions
    {
        /// <summary>
        /// Add Database to Application
        /// </summary>
        /// <param name="services">IServiceCollection</param>
        /// <returns>IServiceCollection</returns>
        public static IServiceCollection AddDatabase(this IServiceCollection services)
        {
            services.AddSingleton(BuildUpdateableDbContext);
            services.AddSingleton(BuildReadOnlyDbContext);

            services.AddSingleton<IDbContextOptionsBuilder, SqlServerDbContextOptionsBuilder>();


            return services;
        }

        /// <summary>
        /// Build DB Context for read write objects within database used for CQRS
        /// </summary>
        /// <param name="services">IServiceProvider</param>
        /// <returns></returns>
        private static IDbContext BuildUpdateableDbContext(IServiceProvider services)
        {
            return BuildDbContext(services, null);
        }

        /// <summary>
        /// Build DBContext for read only queries and is setting objects as no tracking
        /// Advantage here is we wont have to use ADO.Net, Dapper or another ORM to get information
        /// from the database. 
        /// </summary>
        /// <param name="services">IServiceProvider</param>
        /// <returns>ApplicationDbContext</returns>
        private static IReadOnlyDbContext BuildReadOnlyDbContext(IServiceProvider services)
        {
            return BuildDbContext(services, builder =>
            {
                builder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            });
        }

        /// <summary>
        /// Builds the DB Context
        /// </summary>
        /// <param name="services"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        private static ApplicationDbContext BuildDbContext(IServiceProvider services, Action<DbContextOptionsBuilder<ApplicationDbContext>> options)
        {
            SQLitePCL.Batteries.Init(); // Only using so that can create in memory database with SQL Lite
            var configuration = services.GetService<IConfiguration>();
            var optionsBuilder = services.GetService<IDbContextOptionsBuilder>();
            var builder = new DbContextOptionsBuilder<ApplicationDbContext>();

            optionsBuilder.BuildDbOptions(builder, configuration);
            options?.Invoke(builder);

            var context = new ApplicationDbContext(builder.Options);

            context.Database.OpenConnection();
            context.Database.EnsureCreated();

            return context;
        }
    }
}
