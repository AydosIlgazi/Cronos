using Cronos.Application.Validations;
using FluentValidation.AspNetCore;
using Cronos.Application.Data.Configurations;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Reflection;

namespace Cronos.Application
{
    public static class ServiceRegistration
    {
        public static void RegisterApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            services.AddMediatR(assembly);
            services.AddAutoMapper(assembly);
            services.AddDbContext<ApplicationContext>(options => options.UseSqlServer(configuration.GetConnectionString("CronosDb")));
            services.AddValidatorsFromAssemblyContaining<CreateActivityValidator>();
            
            //services.AddValidatorsFromAssemblyContaining<CreateBannerValidator>();




            ////25.09.2022 Irem Kesemen
            //services.AddControllers().AddFluentValidation(

            //    fv => fv.RegisterValidatorsFromAssemblyContaining<BannerValidator>());
            //services.AddControllersWithViews();
            //services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            //services.AddValidatorsFromAssembly(typeof(ServiceRegistration).Assembly);


        }

    }

   
}
