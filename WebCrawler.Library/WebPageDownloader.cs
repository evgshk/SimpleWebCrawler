using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace WebCrawler.Library
{
    /// <summary>
    /// A Web Page HTML Downloader class
    /// </summary>
    public class WebPageDownloader
    {
        private static HttpClient _httpClient = new HttpClient();

        /// <summary>
        /// Send a request to the specified uri and return response body as a string
        /// </summary>
        /// <param name="uri">Uri uri</param>
        public string GetString(string uri)
        {
            string result = _httpClient.GetStringAsync(uri).Result;

            return result;
        }

        /// <summary>
        /// Send a request to the specified uri and return response body as a string
        /// </summary>
        /// <param name="uri">string uri</param>
        public string GetString(Uri uri)
        {
            string result = _httpClient.GetStringAsync(uri).Result;

            return result;
        }
    }
}
