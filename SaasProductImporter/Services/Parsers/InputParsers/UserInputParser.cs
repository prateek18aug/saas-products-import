namespace SaasProductImporter.Services.Parsers.InputParsers
{
    using SaasProductImporter.Application.Configs;
    using System;
    using System.Linq;

    public class UserInputParser : IUserInputParser
    {
        private readonly IAppSettings appSettings;
        public const string FileNameErrorMsg = "Please enter file name in the format \"FolderName/FileName\"";
        public const string FileTypeErrorMsg = "Please enter file name in the format \"FileName.FileType\"";

        public UserInputParser(IAppSettings appSettings)
        {
            this.appSettings = appSettings;
        }

        public string GetFileName(string inputString)
        {
            if (!inputString.Contains('/'))
            {
                throw new ArgumentException(nameof(inputString), FileNameErrorMsg);
            }

            var fileName = inputString.Split('/').LastOrDefault();
            if (string.IsNullOrWhiteSpace(fileName))
            {
                throw new ArgumentException(nameof(inputString), FileNameErrorMsg);
            }

            return fileName;
        }

        public string GetFileType(string fileName)
        {
            if (!fileName.Contains('.'))
            {
                throw new ArgumentException(nameof(fileName), FileTypeErrorMsg);
            }

            var fileType = fileName.Split('.').LastOrDefault();
            if (string.IsNullOrWhiteSpace(fileType))
            {
                throw new ArgumentException(nameof(fileName), FileTypeErrorMsg);
            }

            return fileType;
        }

        public string GetCompanyName(string inputString)
        {
            if (string.IsNullOrWhiteSpace(inputString))
            {
                throw new ArgumentNullException(
                    nameof(inputString),
                    "Input string should not be null or empty");
            }

            var companyName = appSettings.CompanyNames
                .FirstOrDefault(x => inputString.Contains(x))?.ToLower();

            if (string.IsNullOrWhiteSpace(companyName))
            {
                throw new ArgumentException(
                    nameof(inputString),
                    "Please enter a valid company name");
            }

            return companyName;
        }
    }
}
