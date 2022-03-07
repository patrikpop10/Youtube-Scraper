using Microsoft.AspNetCore.Mvc;
using scrapperlib;
using Newtonsoft.Json.Linq;
using scrapper;
using scrapperlib.Models;
namespace YoutubeApi.Controllers
{

    [Route("api/trending")]
    [ApiController]
    public class TrendingController : ControllerBase
    {
        [HttpGet("{countryCode}/{ordered}")]
        public ActionResult<List<Video>> GetTrendingVideos(string countryCode, bool ordered)
        {
            return GetVideos(countryCode, ordered);

        }
        [HttpGet]
        public ActionResult<List<Video>> GetTrendingVideosFromDifferentCountries([FromQuery] List<string> countryCodes, [FromQuery] bool ordered)
        {
            List<Video> trendingFromDifferentCountries = new List<Video>();
            foreach (var countryCode in countryCodes)
            {
                trendingFromDifferentCountries.AddRange(GetVideos(countryCode, ordered));
            }
            return trendingFromDifferentCountries;
        }
        private static List<Video> GetVideos(string countryCode, bool ordered)
        {
            var videos = new List<Video>();
            var item = new Downloader(Program.configuration["trending:endPoint"] + countryCode);
            item.CountryCode = countryCode;
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
                videoIds = videoIds.Distinct().ToList();
                if (ordered)
                {
                    foreach (var id in videoIds)
                    {

                        Collector collector = new Collector(id.ToString(), countryCode, Program.configuration);
                        videos.Add(collector.Collect(id.ToString()));
                    }
                }
                else
                {
                    Parallel.ForEach(videoIds, id =>
                    {
                        Collector collector = new Collector(id.ToString(), countryCode, Program.configuration);
                        videos.Add(collector.Collect(id.ToString()));
                    });
                }
            }
            return videos;
        }


    }
}