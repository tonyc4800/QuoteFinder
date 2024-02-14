namespace QuoteFinderExercise.DataProcessing;
internal interface IQuoteProcessor
{
    IEnumerable<string> GetShortestQuotesContaining(string wordToSearch, IEnumerable<IEnumerable<string>> quoteCollection);
    IEnumerable<string> GetShortestQuotesContainingParallel(string wordToSearch, IEnumerable<IEnumerable<string>> quotesCollection);
}