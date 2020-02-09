using Moq;
using NUnit.Framework;
using SaasProductImporter.Application.Configs;
using SaasProductImporter.Services.Parsers.InputParsers;
using System;
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
        public void ShouldGetFileName_WhenInputContainsSlash()
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
        public void ShouldThrowException_WhenInputDoesNotContainsSlash()
        {
            var input = "import capterra feed-productscapterra.yaml";

            Assert.Throws<ArgumentException>(
                () => userInputParser.GetFileName(input),
                "Please enter file name in the format \"FolderName/FileName\"");
        }

        [Test]
        public void ShouldThrowException_WhenInputDoesNotContainsAnythingAfterSlash()
        {
            var input = "import capterra feed-products/";

            Assert.Throws<ArgumentException>(() =>
            userInputParser.GetFileName(input),
            "Please enter file name in the format \"FolderName/FileName\"");
        }

        [Test]
        public void ShouldGetCompanyName_WhenInputContainsCompanyName()
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

        [Test]
        public void ShouldThrowException_WhenInputStringIsEmpty()
        {
            // Arrange
            var input = string.Empty;

            // Assert
            Assert.Throws<ArgumentNullException>(
                () => userInputParser.GetCompanyName(input),
                "Input string should not be null or empty");
        }

        [Test]
        public void ShouldThrowException_WhenInputCompanyIsNotInAppSettings()
        {
            // Arrange
            var input = "import abc";
            mockAppSettings.SetupGet(x => x.CompanyNames)
                .Returns(new List<string>() { "capterra", "softwareadvice" });

            // Assert
            Assert.Throws<ArgumentException>(
                () => userInputParser.GetCompanyName(input),
                "Please enter a valid company name");
        }


        [Test]
        public void ShouldGetFileType_WhenFileNameContainsDot()
        {
            // Arrange
            var input = "capterra.yaml";
            var expectedFileType = "yaml";

            // Act
            var fileName = userInputParser.GetFileType(input);

            // Assert
            Assert.AreEqual(expectedFileType, fileName);
        }

        [Test]
        public void ShouldThrowException_WhenFileNameDoesNotContainsDot()
        {
            var input = "capterra";

            Assert.Throws<ArgumentException>(
                () => userInputParser.GetFileType(input),
                "Please enter file name in the format \"FileName.FileType\"");
        }

        [Test]
        public void ShouldThrowException_WhenFileNameDoesNotContainsAnythingAfterDot()
        {
            var input = "capterra.";

            Assert.Throws<ArgumentException>(
                () => userInputParser.GetFileType(input),
                "Please enter file name in the format \"FileName.FileType\"");
        }
    }
}
