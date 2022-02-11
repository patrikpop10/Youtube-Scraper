
namespace database
{
        public class Video
    {
        public Video(string videoId, List<string> titles, List<string> descriptions, Youtuber uploader, TimeSpan length, DateTime uploadDate, List<string> availableCountries, bool isFamilyFriendly)
        {
            VideoId = videoId;
            Titles = titles;
            Descriptions = descriptions;
            Uploader = uploader;
            Length = length;
            UploadDate = uploadDate;
            AvailableCountries = availableCountries;
            IsFamilyFriendly = isFamilyFriendly;
        }

        public string VideoId { get; set; }
        public List<string> Titles {get; set;}
        public List<string> Descriptions {get; set;}
        public Youtuber Uploader {get; set;}
        public TimeSpan Length {get; set;}
        public DateTime UploadDate {get; set;}
        public List<string> AvailableCountries {get; set;}
        public bool IsFamilyFriendly {get; set;}
        
        
         
    }
}