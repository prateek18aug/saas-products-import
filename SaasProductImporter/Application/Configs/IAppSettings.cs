namespace SaasProductImporter.Application.Configs
{
    using System.Collections.Generic;

    public interface IAppSettings
    {
        List<string> CompanyNames { get; set; }
        string DatabaseProvider { get; set; }
    }
}
