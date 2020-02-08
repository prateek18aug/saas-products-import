namespace SaasProductImporter.DataAccess
{
    using SaasProductImporter.Models;

    public interface IDataAccess
    {
        void Insert(ProductsRoot productRoot);
    }
}
