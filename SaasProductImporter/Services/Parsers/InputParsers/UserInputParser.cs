﻿namespace SaasProductImporter.Services.Parsers.InputParsers
{
    using SaasProductImporter.Application.Configs;
    using System.Linq;

    public class UserInputParser : IUserInputParser
    {
        private readonly IAppSettings appSettings;
        public UserInputParser(IAppSettings appSettings)
        {
            this.appSettings = appSettings;
        }

        public string GetFileName(string inputString)
        {
            return inputString.Split('/').LastOrDefault();
        }

        public string GetFileType(string inputString)
        {
            return inputString.Split('.').LastOrDefault();
        }

        public string GetCompanyName(string inputString)
        {
            return appSettings.CompanyNames
                .FirstOrDefault(x => inputString.Contains(x)).ToLower();
        }
    }
}
