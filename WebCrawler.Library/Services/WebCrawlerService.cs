using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebCrawler.Library.Core;
using WebCrawler.Library.Core.Web;

namespace WebCrawler.Library.Services
{
    public class WebCrawlerService : IWebCrawlerService
    {
        private readonly HashSet<string> _visitedLinks;
        private readonly Queue<Uri> _linksToVisit;

        private readonly WebCrawlerOptions _options;

        public WebCrawlerService(WebCrawlerOptions options)
        {
            _options = options;

            _visitedLinks = new HashSet<string>();
            _linksToVisit = new Queue<Uri>();
            _linksToVisit.Enqueue(_options.Uri);
        }
        
        /// <summary> Entry point: crawling executing. </summary>
        /// <remarks> The crawling technique based on Breadth-first principle utilizing queue underneath. </remarks>
        public async Task Execute()
        {
            Console.WriteLine("Crawling started.");

            while (_linksToVisit.Count > 0)
            {
                if (_linksToVisit.TryDequeue(out var url))
                {
                    await ProcessFor(url);
                }
            }

            Console.WriteLine("Wooahh! Crawling completed.");
        }

        private async Task ProcessFor(Uri url)
        {
            var htmlResult = await PageDownloader.GetHtml(url);
            
            var extractedLinks = PageParser.ExtractLinks(htmlResult, url);
            _visitedLinks.Add(url?.ToString());
            var addedLinks = EnqueueLinks(extractedLinks);
            
            PersistentManager.SaveAsHtml(htmlResult, url, _options.DestinationPath);
            
            Console.WriteLine($"Visited links: {_visitedLinks.Count}");
            Console.WriteLine($"Links to visit: {_linksToVisit.Count} (+{addedLinks})");
        }

        private int EnqueueLinks(IEnumerable<string> linksToVisit)
        {
            var counter = 0;
            foreach (var link in linksToVisit)
            {
                var url = new Uri(link);
                if (!_visitedLinks.Contains(link) && !_linksToVisit.Contains(url))
                {
                    _linksToVisit.Enqueue(url);
                    counter++;
                }
            }

            return counter;
        }
    }
}
