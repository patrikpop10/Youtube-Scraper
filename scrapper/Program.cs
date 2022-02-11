using System.Threading.Channels;
using Microsoft.Extensions.Configuration;
using scrapper.lib;
using System.Threading.Tasks;

namespace scrapper
{
    public class Program
    {
        public static IConfigurationRoot configuration = new ConfigurationBuilder().AddJsonFile("D:/Projects/Youtube Scrapper/scrapper/appsettings.json", false, true).Build();
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
            // var downloader = new Downloader(root["trending:endPoint"] + "US");
            // var contents = downloader.Dowload();

            // var filter = new TrendingFilterer(contents, root["trending:xPath"], root["trending:javascriptToRemove"]);
            // var ids = filter.Filter("videoId");
            // var a = root;
            // filter.WriteTokensToFile();

            // foreach (var item in ids)
            // {
            //     downloader = new Downloader(root["video:endPoint"] +$"{item}");
            //     var cc = downloader.Dowload();
            //     var videoFilter = new VideoFilterer(cc, root["video:xPath"], root["video:javascriptToRemove"]);
            //     var category = videoFilter.VideoDetails.SelectToken("category").ToString();
            //     Console.WriteLine(category);

        }

        private static object List<T>()
        {
            throw new NotImplementedException();
        }
    }

}

