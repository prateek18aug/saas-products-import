namespace SaasProductImporter.Services.Parsers.InputParsers
{
    public interface IUserInputParser
    {
        string GetFileName(string inputString);
        string GetCompanyName(string inputString);
    }
}
