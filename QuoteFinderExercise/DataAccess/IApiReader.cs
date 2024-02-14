using QuoteFinderExercise.App;
using QuoteFinderExercise.DTO;

namespace QuoteFinderExercise.DataAccess;
internal interface IApiReader
{
    Task<IEnumerable<IEnumerable<string>>> ReadQuotes(QueryFilters searchCriteria);
    Task<IEnumerable<IEnumerable<string>>> ReadQuotesSlow(QueryFilters searchCriteria);
}