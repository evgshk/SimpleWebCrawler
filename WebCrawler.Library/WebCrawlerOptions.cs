using System;
using System.IO;

namespace WebCrawler.Library
{
    /// <summary> Web Crawler Options </summary>
    /// <param name="Uri">Uri to start crawling from. </param>
    /// <param name="DestinationPath">Destination path to save HTML pages. </param>
    public record WebCrawlerOptions(Uri Uri, DirectoryInfo DestinationPath);
}
