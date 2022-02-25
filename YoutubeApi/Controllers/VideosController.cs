using Microsoft.AspNetCore.Mvc;
using scrapperlib;
using Newtonsoft.Json.Linq;
using scrapper;
using scrapperlib.Models;
namespace YoutubeApi.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class VideoController : ControllerBase
    {
        [HttpGet("{id}")]
        public ActionResult<Video> GetVideo(string id)
        {
         var collector = new Collector(id, Program.configuration);
         return collector.Collect(id);
        }

        
        [HttpGet]
        public ActionResult<List<Video>> GetVideos([FromQuery]string[] listOfIds)
        {
            var listOfvideos = new List<Video>();
            foreach(var id in listOfIds)
            {
                var collector = new Collector(id, Program.configuration);
                listOfvideos.Add(collector.Collect(id));
            }
            return listOfvideos;
        }
    }

}