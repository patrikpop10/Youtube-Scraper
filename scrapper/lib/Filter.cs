using HtmlAgilityPack;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace scrapper.lib
{
    public class Filter
    {
        protected HtmlDocument _document { get; set; }
        protected string _documentString { get; set; }
        public string RawHtmlString { get; set; }
        public JArray Tokens { get; set; }
        public string OriginalJString { get; set; }

        public Filter(HtmlDocument document, string where)
        {
            _document = document;
            RawHtmlString = _document.DocumentNode.OuterHtml;
            _documentString = document.DocumentNode.SelectSingleNode(where).InnerText;
            OriginalJString = _documentString;


        }
        public JObject ConvertToJson() => JObject.Parse(_documentString);

        protected void RemoveJavaScript(string remove)
        {
            _documentString = _documentString.Replace(remove, "");
            _documentString = _documentString.Remove(_documentString.Length - 1, 1);
        }

        public List<string> DoFilter(string field) => Tokens.Filter<JToken>(field);

        public void WriteTokensToFile()
        {
            File.WriteAllLines("debug.json", Tokens.Select(t => t.ToString()));
        }


    }
}