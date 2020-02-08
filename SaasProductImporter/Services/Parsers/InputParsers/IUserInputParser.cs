namespace SaasProductImporter.Services.Parsers.InputParsers
{
    public interface IUserInputParser
    {
        string GetFilePath(string inputString);
        string GetCompanyName(string inputString);
    }
}
