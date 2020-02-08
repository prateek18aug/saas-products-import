namespace SaasProductImporter.Services.Parsers.ProductParsers
{
    using SaasProductImporter.Models;

    public interface IProductParser
    {
        ProductsRoot DeserializeFileContent(string fileName);
        void PrintProductDetails(ProductsRoot productsRoot);
        string GetProductDetail(Product product);
    }
}
