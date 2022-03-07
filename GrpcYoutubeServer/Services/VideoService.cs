using Grpc.Core;
using GrpcYoutubeServer;
using scrapperlib;

namespace GrpcYoutubeServer.Services;

public class VideoService : Video.VideoBase
{
    private readonly ILogger<VideoService> _logger;
    public VideoService(ILogger<VideoService> logger)
    {
        _logger = logger;
    }

   public override Task<VideoData> GetVideoInfo(VideoRequest videoRequest, ServerCallContext context)
   {
        var collector = new Collector(videoRequest.VideoId, scrapper.Program.configuration);
        var videoFromScrapper = collector.Collect(videoRequest.VideoId);
        VideoData video = new VideoData(){
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
        return Task.FromResult(video);

   }
}