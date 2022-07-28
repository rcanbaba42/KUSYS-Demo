using KUSYS.Application.Abstracts;
using KUSYS.Persistence.Contexts;
using KUSYS.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace KUSYS.Persistence.Extensions
{
    public static class Registration
    {
        /// <summary>
        /// Infrastructure/Persistence katmanında kullanılan servisler-repositoryler register ediliyor
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IServiceCollection AddInfrastructureRegistration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<KUSYSContext>(conf =>
            {
                var connStr = configuration.GetConnectionString("KUSYS").ToString();
                conf.UseSqlServer(connStr, opt =>
                {
                    opt.EnableRetryOnFailure();
                });
            });
            /*
             başlangıç verilerimiz yüklenmesi için çağrı yapılıyor
             */
            var seed = new SeedData();
            seed.SeedAsync(configuration).GetAwaiter();

            services.AddScoped<IStudentRepository, StudentRepository>();
            services.AddScoped<ICourseRepository, CourseRepository>();
            return services;
        }
    }
}
