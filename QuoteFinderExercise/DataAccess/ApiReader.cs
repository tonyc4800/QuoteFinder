using QuoteFinderExercise.App;
using QuoteFinderExercise.DTO;
using System.Text.Json;

namespace QuoteFinderExercise.DataAccess;
internal class ApiReader : IApiReader
{
    public async Task<IEnumerable<IEnumerable<string>>> ReadQuotes(QueryFilters searchCriteria)
    {
        var tasks = new List<Task<IEnumerable<string>>>();

        for (int pageNumber = 1; pageNumber <= searchCriteria.NumberOfPages; pageNumber++)
        {
            var singlePageOfQuotes = GetQuotesForSinglePage(searchCriteria.QuoteLimit, pageNumber);
            tasks.Add(singlePageOfQuotes);
        }

        Task.WaitAll(tasks.ToArray());
        Console.WriteLine();

        return (await Task.WhenAll(tasks)).ToList();

    }

    public async Task<IEnumerable<IEnumerable<string>>> ReadQuotesSlow(QueryFilters searchCriteria)
    {
        var quotescolletion = new List<IEnumerable<string>>();

        for (int pageNumber = 1; pageNumber <= searchCriteria.NumberOfPages; pageNumber++)
        {
            var singlePageOfQuotes = await GetQuotesForSinglePage(searchCriteria.QuoteLimit, pageNumber);
            quotescolletion.Add(singlePageOfQuotes);
        }
        Console.WriteLine();

        return quotescolletion;
    }

    private async Task<IEnumerable<string>> GetQuotesForSinglePage(int limit, int page)
    {
        using var client = new HttpClient();
        var endpoint = $"https://quote-garden.onrender.com/api/v3/quotes?limit={limit}&page={page}";
        var response = await client.GetAsync(endpoint);

        response.EnsureSuccessStatusCode();

        var json = await response.Content.ReadAsStringAsync();        
        var root = JsonSerializer.Deserialize<Root>(json);

        return root!.data.Select(item => $"{item.quoteText} -- {item.quoteAuthor}");
    }
}
