
namespace database
{
    public class Video
    {
     
        public string VideoId { get; set; }
        public Youtuber Uploader { get; set; }
        public Int64 Length { get; set; }
        public string Country { get; set; }
        public DateTime UploadDate { get; set; }
        public bool IsFamilyFriendly { get; set; }
        public string IFrame { get; set; }
        public Category Category { get; set; }
        public List<VideoCache> Cache { get; set; }
    }
}