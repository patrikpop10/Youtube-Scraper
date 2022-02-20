using HtmlAgilityPack;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace scrapperlib
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
            try
            {
                _document = document;
                RawHtmlString = _document.DocumentNode.OuterHtml;
                _documentString = document.DocumentNode.SelectSingleNode(where).InnerText;
                OriginalJString = _documentString;
            }
            catch
            {

                _documentString = document.DocumentNode.SelectSingleNode("//script[contains(., 'gostos')]")?.InnerText ?? "0";
            }


        }
        public JObject ConvertToJson() => JObject.Parse(_documentString);

        protected void RemoveJavaScript(string remove)
        {
            _documentString = _documentString.Replace(remove, "");
            _documentString = _documentString.Replace("var ytInitialData = ", "");
            _documentString = _documentString.Remove(_documentString.Length - 1, 1);
        }

        public List<string> DoFilter(string field) => Tokens.Filter<JToken>(field);

        public void WriteTokensToFile()
        {
            File.WriteAllLines("debug.json", Tokens.Select(t => t.ToString()));
        }


    }
}