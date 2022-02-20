namespace database
{
    public class YoutuberType
    {
        public Guid Id { get; set; }
        public string type { get; set; }
        public virtual List<Youtuber> Youtubers {get; set;}

    }
}