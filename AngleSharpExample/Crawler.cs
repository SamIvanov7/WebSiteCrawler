using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

public class Crawler
{
    private readonly string baseUrl;
    private readonly HttpClient httpClient;

    public Crawler(string baseUrl)
    {
        this.baseUrl = baseUrl;
        httpClient = new HttpClient();
    }

    public async Task<List<Article>> FetchArticlesAsync()
    {
        var response = await httpClient.GetAsync(baseUrl);
        var pageContent = await response.Content.ReadAsStringAsync();

        var htmlDocument = new HtmlDocument();
        htmlDocument.LoadHtml(pageContent);

        var nodes = htmlDocument.DocumentNode.SelectNodes("//a[contains(@href, '/refactoring/smells')]");
        var articles = new List<Article>();

        foreach (var node in nodes)
        {
            var title = node.InnerText;
            var url = node.GetAttributeValue("href", string.Empty);

            if (!url.StartsWith("http"))
            {
                url = "https://refactoring.guru" + url;
            }

            articles.Add(new Article { Title = title, Url = url });
        }

        return articles;
    }
}
