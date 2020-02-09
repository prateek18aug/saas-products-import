namespace SaasProductImporter.Services.Parsers.ProductParsers
{
    using SaasProductImporter.Application.Factories;
    using SaasProductImporter.Application.Utilities;
    using SaasProductImporter.Models;
    using SaasProductImporter.Services.Parsers.InputParsers;
    using System.Collections.Generic;

    public class CapterraParser : ProductParser, IProductParser
    {
        public CapterraParser(
            ISerializerFactory serializerFactory,
            IUserInputParser userInputParser)
            : base(serializerFactory, userInputParser)
        {
        }

        public override ProductsRoot DeserializeFileContent(string fileName)
        {
            var serializer = ResolveSerializer(fileName);
            var fileContent = FileHelper.GetContent(fileName);

            return new ProductsRoot()
            {
                Products = serializer.Deserialize<List<Product>>(fileContent)
            };
        }

        public override string GetProductDetail(Product product)
        {
            return $"importing: Name: \"{product.Title}\";  Categories:{product.Tags}; Twitter: @{product.Twitter}";
        }
    }
}
