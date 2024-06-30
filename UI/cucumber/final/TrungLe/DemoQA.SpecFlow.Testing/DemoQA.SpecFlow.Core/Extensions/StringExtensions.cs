using System.Reflection;


namespace DemoQA.SpecFlow.Core.Extensions
{
    public static class StringExtensions
    {
        public static string GetAbsolutePath(this string filePath)
        {
            string directoryPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            string fullPath = Path.Combine(directoryPath, filePath);
            if (File.Exists(fullPath))
            {
                return fullPath;
            }

            return string.Empty;
        }

        public static string GetTextFromJsonFile(this string filePath)
        {
            string path = filePath.GetAbsolutePath();
            return File.ReadAllText(path);
        }
    }
}
