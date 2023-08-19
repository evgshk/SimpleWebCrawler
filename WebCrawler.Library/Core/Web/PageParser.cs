using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace WebCrawler.Library.Core.Web
{
    /// <summary> Web Page Parser. </summary>
    public static class PageParser
    {
        private const string UrlPattern = "(?<=<a\\s*?href=(?:'|\"))[^'\"]*?(?=(?:'|\"))";

        /// <summary> Extracts links from a given HTML page. </summary>
        /// <param name="htmlText"> HTML page text. </param>
        /// <param name="url"> Page url. </param>
        public static IEnumerable<string> ExtractLinks(string htmlText, Uri url)
        {
            var extractedLinks = ExtractLinksFrom(htmlText)
                .Where(link => IsValidLink(link, url))
                .Select(link => NormalizeLink(link, url))
                .Distinct();

            return extractedLinks;
        }

        private static IEnumerable<string> ExtractLinksFrom(string htmlText)
        {
            var urlRegex = new Regex(UrlPattern, RegexOptions.Compiled | RegexOptions.IgnoreCase);
            return urlRegex.Matches(htmlText).Select(match => match.Value);
        }

        private static string NormalizeLink(string link, Uri url)
        {
            return link.StartsWith('/')
                ? new Uri(url, link).AbsoluteUri
                : link;
        }

        private static bool IsValidLink(string link, Uri url) 
            => !link.Contains("mailto") && (link.Contains(url.Host) || link.StartsWith('/'));
    }
}
