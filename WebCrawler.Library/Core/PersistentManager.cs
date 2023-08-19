using System;
using System.IO;
using System.Text.RegularExpressions;

namespace WebCrawler.Library.Core
{
    public class PersistentManager
    {
        private const string InvalidCharsReplacement = "_";
        
        /// <summary> Saves an HTML file to disk. </summary>
        public static void SaveAsHtml(string content, Uri fromUrl, DirectoryInfo path)
        {
            var filename = GenerateSafeFilename(fromUrl);
            
            var filePath = Path.Combine(path.FullName, $"{filename}.html");
            File.WriteAllText(filePath, content);
            
            Console.WriteLine($"Saved on disk: {filename}.html");
        }

        /// <summary> Generates safe filename from URL eliminating invalid characters. </summary>
        private static string GenerateSafeFilename(Uri fromUrl)
        {
            var invalidChars = Regex.Escape(new string(Path.GetInvalidFileNameChars()));
            var validName = Regex.Replace(fromUrl.AbsolutePath, $"[{invalidChars}]", InvalidCharsReplacement);

            return validName.Trim('_');
        }
    }
}
