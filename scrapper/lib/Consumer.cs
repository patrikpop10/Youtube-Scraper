using System.Threading.Channels;
using database;
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
                try
                {
                    var item = await _reader.ReadAsync();
                    Console.WriteLine("Reading...");
                    var b = item.Dowload();

                    if (item.HtmlDownloader.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        var filter = new TrendingFilterer(b, Program.configuration["trending:xPath"], Program.configuration["trending:javascriptToRemove"]);
                        filter.WriteTokensToFile();
                        var ids = filter.DoFilter("videoId");
                        Console.WriteLine($"Reading {ids[1]}");

                    }
                    else
                    {
                        Console.WriteLine($"Could not crawl, status code was: {item.HtmlDownloader.StatusCode}");
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }



            }
        }

    }
}