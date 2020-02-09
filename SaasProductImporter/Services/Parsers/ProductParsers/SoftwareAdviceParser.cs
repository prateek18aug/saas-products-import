namespace SaasProductImporter.Services.Parsers.ProductParsers
{
    using SaasProductImporter.Application.Factories;
    using SaasProductImporter.Application.Utilities;
    using SaasProductImporter.Models;
    using SaasProductImporter.Services.Parsers.InputParsers;

    public class SoftwareAdviceParser : ProductParser, IProductParser
    {
        public SoftwareAdviceParser(
            ISerializerFactory serializerFactory,
            IUserInputParser userInputParser)
            : base(serializerFactory, userInputParser)
        {
        }

        public override ProductsRoot DeserializeFileContent(string fileName)
        {
            var fileContent = FileHelper.GetContent(fileName);
            var serializer = ResolveSerializer(fileName);

            return serializer.Deserialize<ProductsRoot>(fileContent);
        }

        public override string GetProductDetail(Product product)
        {
            return $"importing: Name: \"{product.Title}\";  Categories:{string.Join(",", product.Categories)}; Twitter: @{product.Twitter}";
        }
    }
}
