using System;
using scrapper;
using scrapperlib;
namespace test 
{
    internal class Program
    {
        static void Main(string[] args)
        {
             var youtberDownloader = new Downloader(scrapper.Program.configuration["youtuber:endpoint"] +"UC-lHJZR3Gqxm24_Vd_AJ5Yw" +"/videos");
             var data = youtberDownloader.Dowload();
             File.WriteAllText("d.html", data.DocumentNode.OuterHtml);
             VideoFilterer vf = new VideoFilterer(data, scrapper.Program.configuration["youtuber:xPath"],
             scrapper.Program.configuration["trending:javascriptToRemove"], 
             "contents.twoColumnBrowseResultsRenderer.tabs[*].tabRenderer.content.sectionListRenderer.contents[*].itemSectionRenderer.contents[*].gridRenderer.items[*].gridVideoRenderer.videoId", true);
             var b = vf.SelectTokens("contents.twoColumnBrowseResultsRenderer.tabs[*].tabRenderer.content.sectionListRenderer.contents[*].itemSectionRenderer.contents[*].gridRenderer.items[*].gridVideoRenderer.videoId");
             
        }
    }
}