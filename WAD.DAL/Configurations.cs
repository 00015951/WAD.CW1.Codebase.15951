using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using WAD.DAL.Data;
using WAD.DAL.Interfaces;
using WAD.DAL.Repositories;

namespace WAD.DAL
{
    public static class Configurations
    {
        public static IServiceCollection Configs(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IActivityRepository, ActivityRepository>();


            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            return services;
        }
    }
}
