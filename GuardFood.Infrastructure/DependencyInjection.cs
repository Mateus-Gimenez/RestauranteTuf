using GuardFood.Core.Context;
using GuardFood.Core.Data.Interfaces;
using GuardFood.Core.Data.Repository;
using GuardFood.Core.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


//namespace GuardFood.Core
//{
//    public static class DependencyInjection
//    {
//        public static IServiceCollection AddInfrastructure(this IServiceCollection services,
//            IConfiguration configuration)
//        {
//            services.AddDbContext<GFContext>(options =>
//             options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"
//            ), b => b.MigrationsAssembly(typeof(GFContext).Assembly.FullName)));

//            services.AddIdentity<ApplicationUser, IdentityRole>()
//                .AddEntityFrameworkStores<GFContext>()
//                .AddDefaultTokenProviders();

//            services.AddScoped<IRestauranteRepository, RestauranteRepository>();

//            services.ConfigureApplicationCookie(options =>
//                     options.AccessDeniedPath = "/Account/Login");

//            //services.AddScoped<IRestauranteRepository, RestauranteRepository>();
            

//            return services;
//        }
//    }
//}
