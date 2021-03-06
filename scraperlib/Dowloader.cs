using HtmlAgilityPack;

namespace scrapperlib
{
    public class Downloader
    {
        public string Path { get; set; }
        public HtmlWeb HtmlDownloader { get; set; }
        public string CountryCode { get; set; }
        public Downloader(string path)
        {
            Path = path;
            HtmlDownloader = new HtmlWeb();
        }
        public HtmlDocument Dowload()
        {
            return HtmlDownloader.Load(Path);

        }

    }
}