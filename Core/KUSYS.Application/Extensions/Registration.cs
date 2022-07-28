using FluentValidation;
using FluentValidation.AspNetCore;
using KUSYS.Application.Validators;
using KUSYS.Domain.Entities;
using Microsoft.Extensions.DependencyInjection;

namespace KUSYS.Application.Extensions
{
    public static class Registration
    {
        /// <summary>
        /// application katmanında kullanılan servisler register ediliyor
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddApplicationRegistration(this IServiceCollection services)
        {
            services.AddFluentValidationAutoValidation();
            services.AddScoped<IValidator<Student>, StudentValidator>();
            return services;
        }
    }
}
