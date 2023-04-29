using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VatebraAcademy.Core.Dtos;

namespace VatebraAcademy.Services.Implementations
{
    public static class DIServices
    {
        public static void AddServices(this IServiceCollection services)
        {
            services.AddCustomConfiguredAutoMapper();
        }
    }

    public static class CustomAutoMapper
    {
        public static void AddCustomConfiguredAutoMapper(this IServiceCollection services)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MapperProfile());
            });

            var mapper = config.CreateMapper();

            services.AddSingleton(mapper);
        }
    }
}
