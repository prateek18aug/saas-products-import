namespace SaasProductImporter.Services.Parsers.ProductParsers
{
    using SaasProductImporter.Models;
    using System;

    public abstract class ProductParser : IProductParser
    {
        public abstract ProductsRoot DeserializeFileContent(string fileName);

        public abstract string GetProductDetail(Product product);

        public void PrintProductDetails(ProductsRoot productsRoot)
        {
            foreach (var product in productsRoot.Products)
            {
                Console.WriteLine(product);
            }
        }
    }
}
