using System.Threading.Tasks;

namespace WebCrawler.Library.Services
{
    /// <summary> Web Crawler service </summary>
    public interface IWebCrawlerService
    {
        Task Execute();
    }
}
