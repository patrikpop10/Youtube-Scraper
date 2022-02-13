namespace database
{
    public class YoutuberCache
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public long SubscriberCount { get; set; }
        public virtual Youtuber Youtuber {get; set;}

}
}