using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using scrapperlib.Models;
namespace scrapperlib
{
    public class Collector
    {
        private string _id { get; set; }
        private string CountryCode { get; set; }
        private IConfigurationRoot ConfigurationRoot { get; set; }
        public Collector(string id, string code, IConfigurationRoot configuration)
        {
            _id = id;
            CountryCode = code;
            ConfigurationRoot = configuration;
        }
        public Collector(string id, IConfigurationRoot configuration)
        {
            _id = id;
            ConfigurationRoot = configuration;
        }



        public Video Collect(string id)
        {
            var videoDownloader = new Downloader(ConfigurationRoot["video:endPoint"] + id);
            var videoHtml = videoDownloader.Dowload();

            //TODO: REFACTOR FILTERS
            var videoFilter = new VideoFilterer(videoHtml, ConfigurationRoot["video:xPath"],
                                                ConfigurationRoot["video:javascriptToRemove"], "microformat.playerMicroformatRenderer", false);

            //contains likes of the video and subscribers,
            var filterUser = new VideoFilterer(videoHtml, "//script[contains(., '\"subscriberCountText\"')]",
                                               ConfigurationRoot["video:javascriptToRemoveForSubs"],
                                               "contents.twoColumnWatchNextResults.results.results.contents[*].videoSecondaryInfoRenderer.owner.videoOwnerRenderer", true);


            string iFrame, isFamilyFriendly, viewCount, publishDate, category, lengthSeconds, owner, ownerChannelName, title, description;
            CollectData(videoFilter, out iFrame, out isFamilyFriendly, out title, out description, out viewCount, out publishDate, out category, out lengthSeconds, out owner, out ownerChannelName);

            try
            {
                var subsJObject = new JObject();
                if (filterUser.OriginalJString == "" || filterUser.OriginalJString.Contains("ytInitialPlayerResponse"))
                {
                    subsJObject = JObject.Parse("{}");
                }
                else
                {
                   
                    subsJObject = JObject.Parse(filterUser.OriginalJString);

                }

                var subscriberCountText = subsJObject.SelectToken(ConfigurationRoot["video:jsonPaths:subscribers"]) ?? "";
                var likes = subsJObject.SelectToken(ConfigurationRoot["video:jsonPaths:likes"]) ?? "";
                var final = $"\n {iFrame} \n title: {title} \n description: {description} is family friendly: {isFamilyFriendly} \n number of views: {viewCount} \n publish date: {publishDate} \n category: {category} \n length in seconds: {lengthSeconds} \n {ownerChannelName}: {subscriberCountText}";
                //Console.WriteLine(final);

                //var finalObject = JObject.Parse($" \"iframe\": {iFrame}, \"title\": {title}, \"description\": {description}, \"familyFriendly\": {isFamilyFriendly}, \"viewCount\":{viewCount}, publishDate:{publishDate}, \"category\": {category}, \"lengthInSeconds\":{lengthSeconds}, \"channelName\": {ownerChannelName}, \"subscriberCountText\": {subscriberCountText}");

                var video = new Video()
                {
                    VideoId = _id,
                    Length = TimeSpan.FromSeconds(double.Parse(lengthSeconds)),
                    Country = CountryCode,
                    UploadDate = DateTime.Parse(publishDate),
                    IsFamilyFriendly = bool.Parse(isFamilyFriendly),
                    Category = category,
                    IFrame = iFrame,
                    Uploader = new Models.Youtber()
                    {
                        Name = ownerChannelName,
                        Subscribers = subscriberCountText.ToString()
                    },
                    Views = Int64.Parse(viewCount),
                    Likes = likes.ToString(),
                    Title = title,
                    Description = description,


                };

                return video;

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return new Video();
            }

        }

        private void CollectData(
                                        VideoFilterer videoFilter,
                                        out string iFrame,
                                         out string isFamilyFriendly,
                                         out string title,
                                         out string description,
                                         out string viewCount,
                                         out string publishDate,
                                         out string category,
                                         out string lengthSeconds,
                                         out string owner,
                                         out string ownerChannelName)
        {
            iFrame = videoFilter.Details?.SelectToken(ConfigurationRoot["video:jsonPaths:iFrame"])?.ToString() ?? "";
            title = videoFilter.Details.SelectToken(ConfigurationRoot["video:jsonPaths:title"]).ToString();
            isFamilyFriendly = videoFilter.Details?.SelectToken(ConfigurationRoot["video:jsonPaths:isFamilyFriendly"]).ToString() ?? "true";
            description = videoFilter.Details.SelectToken(ConfigurationRoot["video:jsonPaths:description"])?.ToString() ?? "";
            //var availableCountries = videoFilter.Details.SelectTokens(ConfigurationRoot["video:jsonPaths:availableCountries"]).ToString();
            viewCount = videoFilter.Details.SelectToken(ConfigurationRoot["video:jsonPaths:viewCount"]).ToString();
            publishDate = videoFilter.Details.SelectToken(ConfigurationRoot["video:jsonPaths:publishDate"]).ToString();
            category = videoFilter.Details.SelectToken(ConfigurationRoot["video:jsonPaths:category"]).ToString();
            lengthSeconds = videoFilter.Details.SelectToken(ConfigurationRoot["video:jsonPaths:lengthSeconds"]).ToString();
            owner = videoFilter.Details.SelectToken(ConfigurationRoot["video:jsonPaths:externalChannelId"]).ToString();
            ownerChannelName = videoFilter.Details.SelectToken(ConfigurationRoot["video:jsonPaths:ownerChannelName"]).ToString();

        }
        public List<Video> YoutuberCollector(string channelId)
        {
            var videos = new List<Video>();
            var youtberDownloader = new Downloader(ConfigurationRoot["youtuber:endpoint"] + channelId + "/videos");
            var data = youtberDownloader.Dowload();
            VideoFilterer vf = new VideoFilterer(data, ConfigurationRoot["youtuber:xPath"],
            ConfigurationRoot["trending:javascriptToRemove"],
            "contents.twoColumnBrowseResultsRenderer.tabs[*].tabRenderer.content.sectionListRenderer.contents[*].itemSectionRenderer.contents[*].gridRenderer.items[*].gridVideoRenderer.videoId", true);
            var videoIds = vf.SelectTokens("contents.twoColumnBrowseResultsRenderer.tabs[*].tabRenderer.content.sectionListRenderer.contents[*].itemSectionRenderer.contents[*].gridRenderer.items[*].gridVideoRenderer.videoId").Select(token => token.ToString()).ToList();

            Parallel.ForEach(videoIds, id =>
            {
                videos.Add(this.Collect(id));
            }
            );
            return videos;
        }

    }


}