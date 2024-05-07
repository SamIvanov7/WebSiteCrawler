using System;
using System.Threading.Tasks;

class Program
{
    static async Task Main(string[] args)
    {
        var crawler = new Crawler("https://refactoring.guru/refactoring/smells");
        var articles = await crawler.FetchArticlesAsync();

        var dbManager = new DatabaseManager("Data Source=webcrawler.db");
        foreach (var article in articles)
        {
            await dbManager.SaveArticleAsync(article);
            Console.WriteLine($"Saved: {article.Title}");
        }

        Console.WriteLine("Crawling and saving complete.");
    }
}
