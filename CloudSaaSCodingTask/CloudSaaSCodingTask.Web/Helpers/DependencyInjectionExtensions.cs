using AutoMapper;
using CloudSaaSCodingTask.Infrastructure.Data;
using CloudSaaSCodingTask.Web.Mapping;
using Microsoft.EntityFrameworkCore;

namespace CloudSaaSCodingTask.Web.Helpers
{
    public static class DependencyInjectionExtensions
    {
        public static IServiceCollection AddAutoMapper(this IServiceCollection services)
        {
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });

            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);
            return services;
        }

        public static IServiceCollection AddCustomDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            var dbType = configuration["DbType"].ToString();
            //inmemory
            if (dbType == "0")
            {
                services.AddDbContext<ApplicationDbContext>(opt => opt.UseInMemoryDatabase(configuration["DbName"]));
            }
            //sqlite
            else if (dbType == "1")
            {
                services.AddDbContext<ApplicationDbContext>(opt => opt.UseSqlite(configuration.GetConnectionString("DefaultConnectionString")));
            }
            //sql
            else if (dbType == "2")
            {
                services.AddDbContext<ApplicationDbContext>(opt => opt.UseSqlServer(configuration.GetConnectionString("DefaultConnectionString")));
            }

            return services;
        }
    }
}
