using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace WebCrawler.Library
{
    public class WebCrawlerOptions
    {
        public Uri Uri { get; }
        public DirectoryInfo DestinationPath { get; }

        public WebCrawlerOptions(Uri uri, DirectoryInfo path)
        {
            Uri = uri;
            DestinationPath = path;
        }
    }
}
