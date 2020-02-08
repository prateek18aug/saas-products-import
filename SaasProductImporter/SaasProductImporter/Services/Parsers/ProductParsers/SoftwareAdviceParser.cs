namespace SaasProductImporter.Services.Parsers.ProductParsers
{
    using Newtonsoft.Json;
    using SaasProductImporter.Application.Utilities;
    using SaasProductImporter.Models;

    public class SoftwareAdviceParser : ProductParser, IProductParser
    {
        public override ProductsRoot DeserializeFileContent(string fileName)
        {
            var fileContent = FileHelper.GetContent(fileName);
            return JsonConvert.DeserializeObject<ProductsRoot>(fileContent);
        }

        public override string GetProductDetail(Product product)
        {
            return $"importing: Name: \"{product.Title}\";  Categories:{string.Join(",", product.Categories)}; Twitter: @{product.Twitter}";
        }
    }
}
