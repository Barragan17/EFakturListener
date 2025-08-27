using System;
using Microsoft.Extensions.Options;
namespace EFakturCallback.Configuration
{
	public static class StartupConfig
	{
        public static GeneralConfiguration RegisterConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<GeneralConfiguration>(configuration.GetSection("Configuration"));
            var serviceProvider = services.BuildServiceProvider();
            var iop = serviceProvider.GetService<IOptions<GeneralConfiguration>>();

            services.AddSingleton(iop.Value);
            return iop.Value;
        }
    }
}

