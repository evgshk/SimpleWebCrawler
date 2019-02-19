using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace WebCrawler.Library
{
    /// <summary>
    /// A Web Page Parser class
    /// </summary>
    public class WebPageParser
    {
        /// <summary>
        /// Extracts links for the specified HTML page
        /// </summary>
        /// <param name="htmlText">HTML page's text</param>
        /// <param name="uri">Domain's uri</param>
        /// <returns></returns>
        public IEnumerable<string> ExtractLinks(string htmlText, Uri uri)
        {
            // Well I think that parsing Html with Regex is the Evil. Oh yeah it is... Brutal truth...
            // Since I can't use any 3rd party libraties let's suppose I'm done this correctly :) 
            // Obviosly It's not true; In the meantime I have only 4-5 hours to finish this task.  
            // What is going below - it's truly magic. I wrote this so quickly...so it should be refactored
            var uriSearchPattern = "(?<=<a\\s*?href=(?:'|\"))[^'\"]*?(?=(?:'|\"))";

            var linkParser = new Regex(uriSearchPattern, RegexOptions.Compiled | RegexOptions.IgnoreCase);

            IEnumerable<string> result = linkParser.Matches(htmlText)
                .Where(x => x.Value.Contains(uri.Host) || x.Value[0] == '/')
                .Where(x => ! x.Value.Contains("mailto"))
                .Select(x => x.Value)
                .Distinct()
                .AsEnumerable();

            HashSet<string> correctedResult = new HashSet<string>();

            foreach(var item in result)
            {
                if(item[0] == '/')
                {
                    correctedResult.Add("http://" + uri.Host + item);
                }
                else
                {
                    correctedResult.Add(item);
                }
            }

            return correctedResult;
        }
    }
}
