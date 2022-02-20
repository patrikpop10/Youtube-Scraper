namespace scrapperlib.Models
{
    public class Video
    {

        public string VideoId { get; set; }
        public Youtber Uploader { get; set; }
        public TimeSpan Length { get; set; }
        public string Country { get; set; }
        public DateTime UploadDate { get; set; }
        public bool IsFamilyFriendly { get; set; }
        public string IFrame { get; set; }
        public string Category { get; set; }
        public Int64 Views { get; set; }
        public string Likes { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }
}