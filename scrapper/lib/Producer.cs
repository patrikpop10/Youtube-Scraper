using System.Threading.Channels;
using Microsoft.Extensions.Configuration;

namespace scrapper.lib
{
    public class Producer
    {
       private ChannelWriter<Downloader> _writer {get; set;}

        public Producer(ChannelWriter<Downloader> writer)
        {
            _writer = writer;
            Task.WaitAll(Run());
        }

        private async Task Run()
        {
            var r = new Random();
            while(await _writer.WaitToWriteAsync())
            {
                var countries = new List<string>();
                Program.configuration.GetSection("countries_ISO2").Bind(countries);
                foreach(var country in countries)
                {
                    try
                    {
                    var downloader = new Downloader(Program.configuration["trending:endPoint"]+country);
                    Console.WriteLine($"Producing for {country}");
                    await _writer.WriteAsync(downloader);
                    }
                    catch(Exception e)
                    {
                        Console.WriteLine(e);
                    }
                 
                }
                  
            }
        }
    }
}