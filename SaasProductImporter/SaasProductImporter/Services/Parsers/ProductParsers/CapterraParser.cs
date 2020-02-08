namespace SaasProductImporter.Services.Parsers.ProductParsers
{
    using SaasProductImporter.Application.Utilities;
    using SaasProductImporter.Models;
    using System.Collections.Generic;
    using YamlDotNet.Serialization;

    public class CapterraParser : ProductParser, IProductParser
    {
        public override ProductsRoot DeserializeFileContent(string fileName)
        {
            var fileContent = FileHelper.GetContent(fileName);

            var deserializer = new Deserializer();
            var products = deserializer.Deserialize<List<Product>>(fileContent);
            return new ProductsRoot()
            {
                Products = products
            };
        }

        public override string GetProductDetail(Product product)
        {
            return $"importing: Name: \"{product.Title}\";  Categories:{product.Tags}; Twitter: @{product.Twitter}";
        }
    }
}
