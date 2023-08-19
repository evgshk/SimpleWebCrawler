using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace WebCrawler.Library.Core.Web
{
    /// <summary> Web Page HTML Downloader class </summary>
    public static class PageDownloader
    {
        private static readonly HttpClient HttpClient = new();
        
        /// <summary> Returns request response body for a given URL. </summary>
        public static async Task<string> GetHtml(Uri url)
        {
            try
            {
                using (var response = await HttpClient.GetAsync(url))
                {
                    response.EnsureSuccessStatusCode();
                    return await response.Content.ReadAsStringAsync();
                }
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Failed to load resource `{url}`: {ex.Message}");
            }
            
            return HttpClient.GetStringAsync(url).Result;
        }
    }
}
