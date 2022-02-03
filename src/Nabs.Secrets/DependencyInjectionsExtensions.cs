using Microsoft.Extensions.DependencyInjection;

namespace Nabs.Secrets
{
    //HowTo Video: https://youtu.be/f8Hf-YUrC10

    public static class DependencyInjectionsExtensions
    {
        public static IServiceCollection AddSecretsAbstractions(this IServiceCollection services)
        {

            return services;
        }
    }
}