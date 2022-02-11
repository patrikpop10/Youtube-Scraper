using HtmlAgilityPack;
using Newtonsoft.Json.Linq;

namespace scrapper.lib
{
    public class VideoFilterer : Filter
    {
        public JObject VideoDetails {get; set;}
        public VideoFilterer(HtmlDocument document, string where, string remove) : base(document, where)
        {
            RemoveJavaScript(remove);
            VideoDetails = Prepare();
        }
        public JObject Prepare()
        {
            var contents = ConvertToJson().SelectToken("microformat.playerMicroformatRenderer");
            var stringContent = contents.ToString();
            var json = JObject.Parse(stringContent);
            
            return json;
            
        }
    }
}