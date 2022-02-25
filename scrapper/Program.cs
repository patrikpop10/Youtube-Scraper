using System.Threading.Channels;
using Microsoft.Extensions.Configuration;
using scrapper.lib;
using System.Threading.Tasks;
using scrapperlib;

namespace scrapper
{
    public class Program
    {
        public static IConfigurationRoot configuration = new ConfigurationBuilder().AddJsonFile("D:/Projects/Youtube Scrapper/scrapper/appsettings.json", false, true).AddEnvironmentVariables().Build();
        public static void Main(string[] args)
        {
            var options = new BoundedChannelOptions(20);
            options.FullMode = BoundedChannelFullMode.Wait;
            var channel = Channel.CreateBounded<Downloader>(options);

            var producerThread = Task.Run(() =>
            {
                new Producer(channel.Writer);
            });

            var consumers = new List<Task>();
            for (int i = 0; i < int.Parse(configuration["workers"]); i++)
            {
                var consumer = Task.Run(() =>
               {
                   new Consumer(channel.Reader);
               });
                consumers.Add(consumer);
            }

            producerThread.Wait();

            Task.WaitAll(consumers.ToArray());
        }

        private static object List<T>()
        {
            throw new NotImplementedException();
        }
    }

}

