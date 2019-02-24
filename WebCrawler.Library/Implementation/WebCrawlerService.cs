using System;
using System.Collections.Generic;
using System.Text;
using WebCrawler.Library.Interfaces;

namespace WebCrawler.Library
{
    /// <summary>
    /// A Web Crawler Service
    /// </summary>
    public class WebCrawlerService : IWebCrawlerService
    {
        /// <summary>
        /// Http links to visit
        /// </summary>
        private static HashSet<string> _visitedLinks;
        /// <summary>
        /// Visited links
        /// </summary>
        private static Queue<string> _linksToVisit;

        /// <summary>
        /// Web Crawler Options
        /// </summary>
        private WebCrawlerOptions _options { get; set; }

        private WebPageDownloader _resourceDownloader { get; }
        private WebPageParser _resourceParser { get; }
        private WebPageStorer _resourseStorer { get; }

        public WebCrawlerService(WebCrawlerOptions options)
        {
            _options = options;

            _visitedLinks = new HashSet<string>();
            _linksToVisit = new Queue<string>();

            _resourceDownloader = new WebPageDownloader();
            _resourceParser = new WebPageParser();
            _resourseStorer = new WebPageStorer();
        }

        /// <summary>
        /// Entry point of the Web Crowler Service
        /// </summary>
        public void Execute()
        {
            _linksToVisit.Enqueue(_options.Uri.ToString());

            ProceedCrawling();
        }

        /// <summary>
        /// Crawling executing
        /// This method is recursive. The crawling tecnique based on Breadth first principle. 
        /// I use [Queue] collection to handle this principle.
        /// </summary>
        private void ProceedCrawling()
        {
            Console.WriteLine("Crawling started.");

            while (_linksToVisit.Count != 0)
            {
                // dequeue the next link
                _linksToVisit.TryDequeue(out string value);

                // download HTML for a given link & extract urls from there
                var uri = new Uri(value);
                var htmlResult = _resourceDownloader.GetString(uri);
                IEnumerable<string> currLinks = _resourceParser.ExtractLinks(htmlResult, uri);

                // save HTML page to the disk
                _resourseStorer.StoreAsHtml(htmlResult, uri, _options.DestinationPath);
                _visitedLinks.Add(value);

                // queue upcoming links
                foreach (var link in currLinks)
                {
                    if (!_visitedLinks.Contains(link) && !_linksToVisit.Contains(link))
                    {
                        _linksToVisit.Enqueue(link);
                        Console.WriteLine(link);
                    }
                }
            }

            Console.WriteLine("Wooahh! Crawling completed.");
        }
    }
}
