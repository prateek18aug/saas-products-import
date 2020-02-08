namespace SaasProductImporter
{
    using System.IO;
    using System.Reflection;

    public static class Settings
    {
        public static string AppBasePath
        {
            get
            {
                return Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
            }
        }

        public static string ProjectBasePath
        {
            get
            {
                return Path.GetDirectoryName(Path.GetDirectoryName(Directory.GetCurrentDirectory()));
            }
        }

        public static string ConfigPath
        {
            get
            {
                return Path.Combine(AppBasePath, "Configs");
            }
        }

        public static string GetFolderPath(string folderName)
        {
            return Path.Combine(ProjectBasePath, folderName);
        }
    }
}
