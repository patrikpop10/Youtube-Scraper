namespace database
{
    public class VideoCache
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public long Views { get; set; }
        public long Likes { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public virtual Video Video { get; set; }


    }

}