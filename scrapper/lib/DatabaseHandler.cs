using scrapperlib;
using database;

namespace scrapper
{
    public class DatabaseHandler
    {
        public DatabaseContext _context { get; set; }

        public DatabaseHandler(DatabaseContext context)
        {
            _context = context;
        }
        public void InsertVideoData(scrapperlib.Models.Video video)
        {
            if (VideoExistsInDatabase(video.VideoId))
            {
                var videoFromDB = _context.Videos.Where(v => v.VideoId == video.VideoId).FirstOrDefault();
                videoFromDB.Cache.Add(new VideoCache()
                {
                    Id = Guid.NewGuid(),
                    Date = DateTime.UtcNow,
                    Views = video.Views,

                }
                );
            }
            else
            {

            }
        }
        public void InsertYoutuberData(scrapperlib.Models.Youtber youtber)
        {
            if (YoutuberExistsInDatabase(youtber.Url))
            {
                var youtuber = new database.Youtuber();
            }
            else
            {

            }

        }
        private bool VideoExistsInDatabase(string id) => _context.Videos.Where(v => v.VideoId == id).FirstOrDefault() != null ? true : false;
        private bool YoutuberExistsInDatabase(string id) => _context.Youtubers.Where(y => y.AccountUrl == id).FirstOrDefault() != null ? true : false;

    }
}