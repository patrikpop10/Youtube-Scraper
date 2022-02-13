namespace database
{
    public class Youtuber
    {
        public string AccountUrl { get; set; }
        public string UserName { get; set; }
        public virtual List<Video> Videos { get; set; }
        public virtual List<YoutuberCache> YoutuberCache { get; set; }
        
    }
}