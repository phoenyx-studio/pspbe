using System;
using Microsoft.Extensions.DependencyInjection;

namespace pspbe.Service
{
    public static class SluggerExtensions
    {
        public static IServiceCollection AddSlugger(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<Slugger>();
            return serviceCollection;
        }
    }
}