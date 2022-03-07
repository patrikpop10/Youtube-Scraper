using System.Threading.Tasks;
using Grpc.Net.Client;
using GrpcYoutubeClient;

// The port number must match the port of the gRPC server.
using var channel = GrpcChannel.ForAddress("https://localhost:7152");
var videoClient = new Video.VideoClient(channel);
var trendingClient = new Trending.TrendingClient(channel);

var videoRequest = new VideoRequest()
{
    VideoId = "_Iye7890tqQ"
};
var videosRequest = new MultipleTrendingRequest();
videosRequest.TrendingRequest.AddRange(new List<TrendingRequest>()
 {
     new TrendingRequest(){
         TrendingId = "NZ"
     },
     new TrendingRequest()
     {
         TrendingId = "GB"
     },
     new TrendingRequest()
     {
         TrendingId = "US"
     }
 }
);
var videos = await trendingClient.GetMultipleTrendingVideosAsync(videosRequest);
var video = await videoClient.GetVideoInfoAsync(videoRequest);

Console.WriteLine($"{video.Title}");

foreach (var v in videos.VideoData)
{
    Console.WriteLine($"{v.Title} \n {v.Country}");
}


