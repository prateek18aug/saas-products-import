namespace SaasProductImporter.Application.Configs
{
    using Microsoft.Extensions.Configuration;
    using System.Collections.Generic;

    public class AppSettings : IAppSettings
    {
        public List<string> CompanyNames { get; set; }
        public string DatabaseProvider { get; set; }

        public AppSettings(IConfigurationRoot configuration)
        {
            configuration.Bind(this);
        }
    }
}
