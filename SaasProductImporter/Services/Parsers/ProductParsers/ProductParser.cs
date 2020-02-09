namespace SaasProductImporter.Services.Parsers.ProductParsers
{
    using SaasProductImporter.Application.Factories;
    using SaasProductImporter.Application.Serializers;
    using SaasProductImporter.Models;
    using SaasProductImporter.Services.Parsers.InputParsers;
    using System;

    public abstract class ProductParser : IProductParser
    {
        private readonly ISerializerFactory serializerFactory;
        private readonly IUserInputParser userInputParser;

        public ProductParser(
            ISerializerFactory serializerFactory,
            IUserInputParser userInputParser)
        {
            this.serializerFactory = serializerFactory;
            this.userInputParser = userInputParser;
        }

        public abstract ProductsRoot DeserializeFileContent(string fileName);

        public abstract string GetProductDetail(Product product);

        protected ISerializer ResolveSerializer(string fileName)
        {
            var fileType = userInputParser.GetFileType(fileName);
            return serializerFactory.Create(fileType);
        }

        public void PrintProductDetails(ProductsRoot productsRoot)
        {
            foreach (var product in productsRoot.Products)
            {
                Console.WriteLine(GetProductDetail(product));
            }
        }
    }
}
