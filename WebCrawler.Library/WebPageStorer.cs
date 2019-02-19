using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace WebCrawler.Library
{
    /// <summary>
    /// A Web Page Storer class
    /// </summary>
    public class WebPageStorer
    {
        /// <summary>
        /// Saves an HTML file to the disk
        /// </summary>
        /// <param name="htmlText">HTML text representation</param>
        /// <param name="uri">Page's uri</param>
        public void StoreAsHtml(string htmlText, Uri uri, DirectoryInfo pathToSave)
        {
            // remove slashes from the absolute path to create a filename
            var slashPattern = new Regex("[/]");
            var filename = slashPattern.Replace(uri.AbsolutePath, "_");

            // save file
            File.WriteAllText(pathToSave + filename + ".html", htmlText);
        }
    }
}
