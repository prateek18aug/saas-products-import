namespace SaasProductImporter.Application.Utilities
{
    using System.IO;

    public class FileHelper
    {
        public static string GetContent(string fileName)
        {
            var folderPath = Settings.GetFolderPath("FeedProducts");
            var completePath = Path.Combine(folderPath, fileName);
            return File.ReadAllText(completePath);
        }
    }
}
