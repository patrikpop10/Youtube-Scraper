using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace scrapper.lib
{
public static class MyExtensions 
{
    
    public static List<string> Filter<Tokens>(this List<JToken> list, string field)
    {   
        
         return list.Select(v => (string) v.SelectToken(field).ToString()).ToList();
            
            
    }
}

}