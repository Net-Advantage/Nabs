using Microsoft.Extensions.DependencyInjection;
using System;

namespace Nabs.Persistence.RelationalPersistence
{
    public static class DependencyInversionExtensions
    {
        public static IServiceCollection AddRelationalPersistence(this IServiceCollection services)
        {



            return services;
        }
    }
}
