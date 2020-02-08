using Moq;
using NUnit.Framework;
using SaasProductImporter.Application.Configs;
using SaasProductImporter.Services.Parsers.InputParsers;
using System.Collections.Generic;

namespace SaasProductImporter.Tests.Services.Parsers.InputParsers
{
    [TestFixture]
    public class UserInputParserTests
    {
        private Mock<IAppSettings> mockAppSettings;
        private UserInputParser userInputParser;

        [SetUp]
        public void TestInitialize()
        {
            mockAppSettings = new Mock<IAppSettings>();
            userInputParser = new UserInputParser(mockAppSettings.Object);
        }

        [Test]
        public void ShouldGetFileNameWhenInputContainsSlash()
        {
            // Arrange
            var input = "import capterra feed-products/capterra.yaml";
            var expectedFileName = "capterra.yaml";

            // Act
            var fileName = userInputParser.GetFileName(input);

            // Assert
            Assert.AreEqual(expectedFileName, fileName);
        }

        [Test]
        public void ShouldGetCompanyNameWhenInputContainsCompanyName()
        {
            // Arrange
            var input = "import capterra feed-products/capterra.yaml";
            var expectedCompanyName = "capterra";
            mockAppSettings.SetupGet(x => x.CompanyNames)
                .Returns(new List<string>() { "capterra", "softwareadvice" });

            // Act
            var companyName = userInputParser.GetCompanyName(input);

            // Assert
            Assert.AreEqual(expectedCompanyName, companyName);
        }
    }
}
