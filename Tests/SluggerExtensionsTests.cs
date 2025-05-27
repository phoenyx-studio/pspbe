using Microsoft.Extensions.DependencyInjection;
using Xunit;
using pspbe.Service;

namespace pspbe.Tests
{
    public class SluggerExtensionsTests
    {
        [Fact]
        public void AddSlugger_RegistersSluggerService()
        {
            var services = new ServiceCollection();
            services.AddSlugger();

            var serviceProvider = services.BuildServiceProvider();
            var slugger = serviceProvider.GetService<Slugger>();

            Assert.NotNull(slugger);
            Assert.IsType<Slugger>(slugger);
        }

        [Fact]
        public void AddSlugger_RegistersAsTransient()
        {
            var services = new ServiceCollection();
            services.AddSlugger();

            var serviceProvider = services.BuildServiceProvider();
            var slugger1 = serviceProvider.GetService<Slugger>();
            var slugger2 = serviceProvider.GetService<Slugger>();

            Assert.NotNull(slugger1);
            Assert.NotNull(slugger2);
            Assert.NotSame(slugger1, slugger2);
        }
    }
}