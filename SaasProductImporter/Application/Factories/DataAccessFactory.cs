namespace SaasProductImporter.Application.Factories
{
    using Autofac;
    using SaasProductImporter.Application.Configs;
    using SaasProductImporter.DataAccess;

    public class DataAccessFactory : IDataAccessFactory
    {
        private readonly IAppSettings appSettings;
        private readonly IComponentContext componentContext;

        public DataAccessFactory(IComponentContext componentContext,IAppSettings appSettings)
        {
            this.appSettings = appSettings;
            this.componentContext = componentContext;
        }

        public IDataAccess Create()
        {
            return componentContext.ResolveKeyed<IDataAccess>(appSettings.DatabaseProvider);
        }
    }
}
