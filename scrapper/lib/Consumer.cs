using System.Threading.Channels;
using database;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

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

                    Console.WriteLine("Reading...");
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
                        foreach (var id in videoIds)
                        {
                          
                          Collector collector = new Collector(id.ToString());
                          collector.Collect();
                          //Collector.Persist();
                        }
                    
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

    }
}