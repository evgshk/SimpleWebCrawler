using System;
using System.IO;
using WebCrawler.Library;

namespace WebCrawler.App
{
    class Program
    {
        static void Main(string[] args)
        {
            // initialize our settings
            var uriToStart = new Uri("http://neveling.net/");
            var directoryToSave = new DirectoryInfo(@"C:\web-crawler-results\");

            // initialize WebCrawlerService
            var options = new WebCrawlerOptions(uriToStart, directoryToSave);
            var crawlerService = new WebCrawlerService(options);

            // go for it!
            crawlerService.Execute();

            Console.ReadLine();
        }
    }
}
