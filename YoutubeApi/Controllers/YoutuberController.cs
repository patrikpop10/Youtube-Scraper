using Microsoft.AspNetCore.Mvc;
using scrapperlib;
using Newtonsoft.Json.Linq;
using scrapper;
using scrapperlib.Models;
namespace YoutubeApi.Controllers
{

    [Route("api/youtuber")]
    [ApiController]
    public class YoutuberController : ControllerBase
    {
        [HttpGet("{id}")]
        public ActionResult<List<Video>> GetYoutuberVideos(string id)
        {
            var collector = new Collector(id, Program.configuration);
            return collector.YoutuberCollector(id);
        }
    }
}