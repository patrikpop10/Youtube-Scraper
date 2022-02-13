using System.Threading.Channels;
using Microsoft.Extensions.Configuration;

namespace scrapper.lib
{
    public class Producer
    {
        private ChannelWriter<Downloader> _writer { get; set; }

        public Producer(ChannelWriter<Downloader> writer)
        {
            _writer = writer;
            Task.WaitAll(Run());
        }

        private async Task Run()
        {

            while (await _writer.WaitToWriteAsync())
            {
                var countries = new List<string>();
                Program.configuration.GetSection("countries_ISO2").Bind(countries);
                foreach (var country in countries)
                {
                    try
                    {
                        var downloader = new Downloader(Program.configuration["trending:endPoint"] + country);
                        downloader.CountryCode = country;
                        Console.WriteLine($"Producing for {country}");
                        await _writer.WriteAsync(downloader);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                    }

                }
                Console.WriteLine("*********************");
                Console.WriteLine("SLEEPING FOR 1 Minute");
                Console.WriteLine("*********************");
                Thread.Sleep((int)TimeSpan.FromMinutes(1).TotalMilliseconds);

            }
        }
    }
}