using Newtonsoft.Json.Linq;

namespace scrapper.lib
{
    public class Collector
    {
        private string _id { get; set; }
        public Collector(string id)
        {
            _id = id;
        }
        public void Collect()
        {
            var videoDownloader = new Downloader(Program.configuration["video:endPoint"] + _id);
            var videoHtml = videoDownloader.Dowload();
            
            //TODO: REFACTOR FILTERS
            var videoFilter = new VideoFilterer(videoHtml, Program.configuration["video:xPath"],
                                                Program.configuration["video:javascriptToRemove"], "microformat.playerMicroformatRenderer");

            var filterUser = new VideoFilterer(videoHtml, "//script[contains(., '\"subscriberCountText\"')]",
                                               Program.configuration["video:javascriptToRemoveForSubs"],
                                               "contents.twoColumnWatchNextResults.results.results.contents[*].videoSecondaryInfoRenderer.owner.videoOwnerRenderer");

            var iFrame = videoFilter.Details.SelectToken(Program.configuration["video:jsonPaths:iFrame"]).ToString();
            var isFamilyFriendly = videoFilter.Details.SelectToken(Program.configuration["video:jsonPaths:isFamilyFriendly"]).ToString();
            var availableCountries = videoFilter.Details.SelectTokens(Program.configuration["video:jsonPaths:availableCountries"]).ToString();
            var viewCount = videoFilter.Details.SelectToken(Program.configuration["video:jsonPaths:viewCount"]).ToString();
            var publishDate = videoFilter.Details.SelectToken(Program.configuration["video:jsonPaths:publishDate"]).ToString();
            var category = videoFilter.Details.SelectToken(Program.configuration["video:jsonPaths:category"]).ToString();
            var lengthSeconds = videoFilter.Details.SelectToken(Program.configuration["video:jsonPaths:lengthSeconds"]).ToString();
            var owner = videoFilter.Details.SelectToken(Program.configuration["video:jsonPaths:ownerProfileUrl"]).ToString();

            var subsJObject = JObject.Parse(filterUser.OriginalJString);

            var final = $"{iFrame} \n {isFamilyFriendly} \n {viewCount} \n {publishDate} \n {category} \n {lengthSeconds}";

            Console.WriteLine(final);

        }

        internal static void Persist()
        {
            throw new NotImplementedException();
        }
    }
}