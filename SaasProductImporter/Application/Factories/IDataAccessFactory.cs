namespace SaasProductImporter.Application.Factories
{
    using SaasProductImporter.DataAccess;

    public interface IDataAccessFactory
    {
        IDataAccess Create();
    }
}
