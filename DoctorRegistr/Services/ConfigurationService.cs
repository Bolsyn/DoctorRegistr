using Microsoft.Extensions.Configuration;
using System.Data.Common;
using System.Data.SqlClient;

namespace DoctorRegistr.Services
{
    public static class ConfigurationService
    {
        public static IConfigurationRoot Configuration { get; private set; }

        public static void Init()
        {
            if (DbProviderFactories.GetFactory("DoctorRegistrProvider") == null)
            {
                DbProviderFactories.RegisterFactory("DoctorRegistrProvider", SqlClientFactory.Instance);
            }

            if (Configuration == null)
            {
                var configurationBuilder = new ConfigurationBuilder();
                Configuration = configurationBuilder.AddJsonFile("appSettings.json").Build();
            }
        }
    }
}
