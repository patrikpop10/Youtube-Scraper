using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace scrapperlib
{
    public static class MyExtensions
    {

        public static List<string> Filter<Tokens>(this JArray list, string field)
        {
            var a = list.Select(v => (string)v.SelectToken(field).ToString()).ToList();
            return a;
        }

        public static List<JObject> Filter(this JObject obj, string arrayPath ,List<string> fields) => obj.SelectTokens(arrayPath + fields.UnpackListToString())
                                                                                                            .Select(v => (JProperty)v.Parent)
                                                                                                            .GroupBy(p => p.Parent)
                                                                                                            .Select(g => new JObject(g))
                                                                                                            .ToList();

        public static string UnpackListToString(this List<string> list)
        {
            var unpackedList = "[";
            foreach (var val in list)
            {
                unpackedList += val.Prepend("'").Append(",");
            }
            unpackedList = unpackedList.Replace(",]","]");
            return unpackedList += "]";
        }
        public static string Prepend(this string s, string antecedent) => antecedent + s;
        public static string Append(this string s, string consequent) => s + consequent;



    }

}