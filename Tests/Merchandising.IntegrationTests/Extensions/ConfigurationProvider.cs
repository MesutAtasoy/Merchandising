using Microsoft.Extensions.Configuration;

namespace Merchandising.IntegrationTests.Extensions;

public class ConfigurationProvider
{
    public static IConfiguration GetConfiguration()
    {
        var configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build();
            
        return configuration;
    }
}