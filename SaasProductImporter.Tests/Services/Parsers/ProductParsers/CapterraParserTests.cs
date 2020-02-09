using Moq;
using NUnit.Framework;
using SaasProductImporter.Application.Factories;
using SaasProductImporter.Models;
using SaasProductImporter.Services.Parsers.InputParsers;
using SaasProductImporter.Services.Parsers.ProductParsers;
using System.Collections.Generic;

namespace SaasProductImporter.Tests.Services.Parsers.ProductParsers
{
    [TestFixture]
    public class CapterraParserTests
    {
        private Mock<ISerializerFactory> mockSerializerFactory;
        private Mock<IUserInputParser> mockUserInputParser;
        private CapterraParser capterraParser;

        [SetUp]
        public void TestInitialize()
        {
            mockSerializerFactory = new Mock<ISerializerFactory>();
            mockUserInputParser = new Mock<IUserInputParser>();
            capterraParser = new CapterraParser(mockSerializerFactory.Object, mockUserInputParser.Object);
        }

        [Test]
        public void ShouldReturnValidProductDetails()
        {
            var product = new Product()
            {
                Title = "xyz",
                Tags = "x,y,z",
                Twitter = "@tw"
            };

            var productDetails = capterraParser.GetProductDetail(product);
            Assert.AreEqual("importing: Name: \"xyz\";  Categories:x,y,z; Twitter: @@tw", productDetails);
        }
    }
}
