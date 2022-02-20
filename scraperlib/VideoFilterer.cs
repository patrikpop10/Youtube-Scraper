using HtmlAgilityPack;
using Newtonsoft.Json.Linq;

namespace scrapperlib
{
    public class VideoFilterer : Filter
    {
        public JObject Details { get; set; }
        public VideoFilterer(HtmlDocument document, string where, string remove, string path, bool multiple) : base(document, where)
        {
            RemoveJavaScript(remove);
            OriginalJString = _documentString;
            Details = Prepare(path, multiple);

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
            JObject json = new JObject();
            try
            {
                if (multiple)
                {
                    var contents = ConvertToJson().SelectToken(jsonPath);
                    var stringContent = contents.ToString();
                    json = JObject.Parse(stringContent);
                    return json;
                }
                else
                {
                    var contents = ConvertToJson().SelectToken(jsonPath);
                    var stringContent = contents.ToString();
                    json = JObject.Parse(stringContent);

                    return json;
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return json;
            

        }
    }
}