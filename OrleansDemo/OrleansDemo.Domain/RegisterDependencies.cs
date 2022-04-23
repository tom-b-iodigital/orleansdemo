using Microsoft.Extensions.DependencyInjection;
using OrleansDemo.Domain.Services;
using OrleansDemo.Domain.Services.Implementations;
using OrleansDemo.Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrleansDemo.Domain
{
    public static class RegisterDependencies
    {
        public static IServiceCollection RegisterCommonDependencies(this IServiceCollection serviceCollection)
        {
            serviceCollection
                .AddTransient<IUniqueNameService, UniqueNameService>()
                .AddTransient<IndexViewModel>();

            return serviceCollection;
        }
    }
}
