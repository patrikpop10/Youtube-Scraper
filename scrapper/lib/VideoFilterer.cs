using HtmlAgilityPack;
using Newtonsoft.Json.Linq;

namespace scrapper.lib
{
    public class VideoFilterer : Filter
    {
        public JObject Details { get; set; }
        public VideoFilterer(HtmlDocument document, string where, string remove, string path) : base(document, where)
        {
            RemoveJavaScript(remove);
            OriginalJString = _documentString;
            Details = Prepare(path, false);

        }
        public JObject Prepare(string jsonPath)
        {

            var contents = ConvertToJson().SelectToken(jsonPath);
            var stringContent = contents.ToString();
            var json = JObject.Parse(stringContent);

            return json;
        }
        public JObject Prepare(string jsonPath, bool multiple)
        {
            if (multiple)
            {
                var contents = ConvertToJson().SelectTokens(jsonPath);
                var stringContent = contents.ToString();
                var json = JObject.Parse(stringContent);
                File.WriteAllText("data/debugYoutuber.json", stringContent);
                return json;
            }
            else
            {
                var contents = ConvertToJson().SelectToken(jsonPath);
                var stringContent = contents.ToString();
                var json = JObject.Parse(stringContent);
                File.WriteAllText("data/debugYoutuber.json", stringContent);
                return json;
            }


        }
    }
}