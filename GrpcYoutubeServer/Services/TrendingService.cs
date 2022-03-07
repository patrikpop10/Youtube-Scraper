using Grpc.Core;
using GrpcYoutubeServer;
using Newtonsoft.Json.Linq;
using scrapperlib;

namespace GrpcYoutubeServer.Services;

public class TrendingService : Trending.TrendingBase
{
    private readonly ILogger<TrendingService> _logger;
    public TrendingService(ILogger<TrendingService> logger)
    {
        _logger = logger;
    }

    public override Task<TrendingPageVideos> GetTrendingVideos(TrendingRequest trending, ServerCallContext context)
    {
        var trendingFromCountry = new TrendingPageVideos();
        trendingFromCountry.VideoData.AddRange(CollectVideosFromCountry(trending));
        return Task.FromResult(trendingFromCountry);
    }
    public override Task<TrendingPageVideos> GetMultipleTrendingVideos(MultipleTrendingRequest multipleTrending, ServerCallContext context)
    {
        var trendingFromMultipleCountries = new TrendingPageVideos();
        foreach(var countryCode in multipleTrending.TrendingRequest)
        {
            trendingFromMultipleCountries.VideoData.AddRange(CollectVideosFromCountry(countryCode));
        }
        return Task.FromResult(trendingFromMultipleCountries);
    }

    private static List<VideoData> CollectVideosFromCountry(TrendingRequest trending)
    {
       
        var videos = new List<VideoData>();
        var item = new Downloader(scrapper.Program.configuration["trending:endPoint"] + trending.TrendingId);
        item.CountryCode = trending.TrendingId;
        var b = item.Dowload();

        if (item.HtmlDownloader.StatusCode == System.Net.HttpStatusCode.OK)
        {
            var filter = new TrendingFilterer(b, scrapper.Program.configuration["trending:xPath"], scrapper.Program.configuration["trending:javascriptToRemove"]);

            var code = JObject.Parse(filter.OriginalJString).SelectToken("topbar.desktopTopbarRenderer.countryCode")?.ToString() ?? "US";

            if (item.CountryCode != code)
            {
                throw new Exception("Not the wanted country");
            }
            var videoIds = filter.Tokens.SelectTokens("trendingArray[*].videoRenderer.videoId").ToList();
            videoIds = videoIds.Distinct().ToList();

            Parallel.ForEach(videoIds, id =>
            {
                Collector collector = new Collector(id.ToString(), trending.TrendingId, scrapper.Program.configuration);
                var videoFromScrapper = collector.Collect(id.ToString());
                VideoData video = new VideoData()
                {
                    VideoId = videoFromScrapper.VideoId,
                    Uploader = new Uploader()
                    {
                        Name = videoFromScrapper.Uploader.Name,
                        Url = videoFromScrapper.Uploader.Url ?? "",
                        Subscribers = videoFromScrapper.Uploader.Subscribers
                    },
                    Length = videoFromScrapper.Length.ToString(),
                    Country = videoFromScrapper.Country ?? "",
                    UploadDate = videoFromScrapper.UploadDate.ToString(),
                    IsFamilyFriendly = videoFromScrapper.IsFamilyFriendly,
                    IFrame = videoFromScrapper.IFrame,
                    Category = videoFromScrapper.Category,
                    Views = videoFromScrapper.Views,
                    Likes = videoFromScrapper.Likes,
                    Title = videoFromScrapper.Title,
                    Description = videoFromScrapper.Description
                };
                videos.Add(video);
            });
           
            return videos;

        }
        else
        {
            return videos;
        }
    }
}