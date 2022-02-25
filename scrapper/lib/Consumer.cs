using System.Threading.Channels;
using database;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using scrapperlib;

namespace scrapper.lib
{
    public class Consumer
    {
        private ChannelReader<Downloader> _reader { get; set; }
        public Consumer(ChannelReader<Downloader> reader)
        {
            _reader = reader;
            Task.WaitAll(Run());
        }
        public async Task Run()
        {
            while (await _reader.WaitToReadAsync())
            {
                var item = await _reader.ReadAsync();
                try
                {
                    var b = item.Dowload();

                    if (item.HtmlDownloader.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        var filter = new TrendingFilterer(b, Program.configuration["trending:xPath"], Program.configuration["trending:javascriptToRemove"]);

                        var code = JObject.Parse(filter.OriginalJString).SelectToken("topbar.desktopTopbarRenderer.countryCode")?.ToString() ?? "US";

                        if (item.CountryCode != code)
                        {
                            throw new Exception("Not the wanted country");
                        }
                        var videoIds = filter.Tokens.SelectTokens("trendingArray[*].videoRenderer.videoId").ToList();

                        Consume(videoIds, item.CountryCode);
                    }
                    else
                    {
                        Console.WriteLine($"Could not crawl, status code was: {item.HtmlDownloader.StatusCode}");
                    }
                }
                catch (Exception e)
                {
                    if (e.Message != "Not the wanted country")
                    {
                        var b = item.CountryCode;
                        Console.WriteLine(e);
                    }

                }
            }
        }

        private static void Consume(List<JToken> videoIds, string countryCode)
        {
            foreach (var id in videoIds)
            {
                Collector collector = new Collector(id.ToString(), countryCode, Program.configuration);
                collector.Collect(id.ToString());
            }
        }

        private static void ConsumeInParallel(List<JToken> videoIds, string countryCode)
        {
            Parallel.ForEach(videoIds, id =>
            {
                //Pass db instance
                Collector collector = new Collector(id.ToString(), countryCode, Program.configuration);
                collector.Collect(id.ToString());
            });
        }
    }
}