using System;
using System.IO;
using System.Threading.Tasks;
using WebCrawler.Library;
using WebCrawler.Library.Services;

namespace WebCrawler.App
{
    internal static class Program
    {
        private static async Task Main(string[] args)
        {
            var uriToStart = new Uri("https://www.cp.pt/passageiros/pt");
            var directoryToSave = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory);
            
            var options = new WebCrawlerOptions(uriToStart, directoryToSave);
            var crawler = new WebCrawlerService(options);
            
            await crawler.Execute();

            Console.ReadLine();
        }
    }
}
