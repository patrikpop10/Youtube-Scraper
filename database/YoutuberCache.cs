namespace database
{
    public class YoutuberCache
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public Int64 SubscriberCount { get; set; }
        public virtual Youtuber Youtuber {get; set;}

}
}