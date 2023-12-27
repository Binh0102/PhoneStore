using Microsoft.Extensions.DependencyInjection;
using ShopPhone.Domain.Repositories;
using ShopPhone.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopPhone.Infrastructure.Modules
{
    public static class InfrastructureModules
    {
        public static IServiceCollection AddInfrastructureModules(this IServiceCollection services)
        {
            services.AddScoped<ICategoryRepo, CategoryRepo>();
            services.AddScoped<IOrderRepo, OrderRepo>();
            services.AddScoped<IOrderdetailRepo, OrderdetailRepo>();
            services.AddScoped<IProductRepo, ProductRepo>();
            services.AddScoped<IUserRepo, UserRepo>();
            return services;
        }
    }
}
