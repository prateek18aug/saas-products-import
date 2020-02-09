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
    public class SoftwareAdviceParserTests
    {
        private Mock<ISerializerFactory> mockSerializerFactory;
        private Mock<IUserInputParser> mockUserInputParser;
        private SoftwareAdviceParser softwareAdviceParser;

        [SetUp]
        public void TestInitialize()
        {
            mockSerializerFactory = new Mock<ISerializerFactory>();
            mockUserInputParser = new Mock<IUserInputParser>();
            softwareAdviceParser = new SoftwareAdviceParser(mockSerializerFactory.Object, mockUserInputParser.Object);
        }

        [Test]
        public void ShouldReturnValidProductDetails()
        {
            var product = new Product()
            {
                Title = "xyz",
                Categories = new List<string>() {"x", "y", "z" },
                Twitter = "@tw"
            };

            var productDetails = softwareAdviceParser.GetProductDetail(product);
            Assert.AreEqual("importing: Name: \"xyz\";  Categories:x,y,z; Twitter: @@tw", productDetails);
        }

        // should throw exception???
    }
}
