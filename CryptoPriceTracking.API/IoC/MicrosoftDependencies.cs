using CryptoPriceTracking.API.Services;

namespace CryptoPriceTracking.API.IoC;

public static class MicrosoftDependencies
{
    public static void AddCustomDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            
            services.AddTransient<ICryptoService, CryptoService>();
        }
}
