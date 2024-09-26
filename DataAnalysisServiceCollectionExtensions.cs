using DataAnalysis.NET.Abstractions;
using DataAnalysis.NET.Core;
using Microsoft.Extensions.DependencyInjection;

namespace DataAnalysis.NET
{
    /// <summary>
    /// Service life time for data analysis extensions
    /// </summary>
    public static class DataAnalysisServiceCollectionExtensions
    {
        /// <summary>
        /// Singleton services for data anlysis extension
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddDataAnalysis(this IServiceCollection services)
        {
            services.AddSingleton<IDataAnalysis, DataAnalysisEngine>();
            return services;
        }

        /// <summary>
        /// Scoped service for data analysis
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddDataAnalysisScoped(this IServiceCollection services)
        {
            services.AddScoped<IDataAnalysis, DataAnalysisEngine>();
            return services;
        }

        /// <summary>
        /// Transient service for data analysis.
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddDataAnalysisTransient(this IServiceCollection services)
        {
            services.AddTransient<IDataAnalysis, DataAnalysisEngine>();
            return services;
        }
    }
}
