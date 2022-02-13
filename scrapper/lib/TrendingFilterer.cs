using HtmlAgilityPack;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace scrapper.lib
{
    public class TrendingFilterer : Filter
    {
        public JObject Tokens {get; set;}
        public TrendingFilterer(HtmlDocument document, string where, string remove) : base(document, where)
        {
            RemoveJavaScript(remove);
            OriginalJString = _documentString;
            Tokens = Prepare();
        }



        private JObject Prepare()
        {
            var contents = ConvertToJson().SelectToken("contents.twoColumnBrowseResultsRenderer.tabs[0].tabRenderer.content.sectionListRenderer.contents")
                                            ?.ToList();

            contents = contents.Select(m => (JToken)m.SelectToken("itemSectionRenderer.contents[0].shelfRenderer.content.expandedShelfContentsRenderer.items"))
                                .ToList();

            var json = JsonConvert.SerializeObject(contents);
            
            json = json.Replace("[[", "[");
            json = json.Replace("]]", "]");
            json = json.Replace("],[", ",");
            json = json.Replace("],null,[", ",");
            json = json.Prepend("{\"trendingArray\" :");
            json = json.Append("}");
            File.WriteAllText("./data/debug2.json", json);
            return JObject.Parse(json);
             
        }
    }
}