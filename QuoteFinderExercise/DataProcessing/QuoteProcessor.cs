namespace QuoteFinderExercise.DataProcessing;
internal class QuoteProcessor : IQuoteProcessor
{
    public IEnumerable<string> GetShortestQuotesContaining(string wordToSearch, IEnumerable<IEnumerable<string>> quotesCollection)
    {
        var shortestQuotes = new List<string>();
        foreach (var collection in quotesCollection)
        {
            string shortestQuote = GetShortestQuoteContaining(wordToSearch, collection);

            if (shortestQuote is null)
            {
                shortestQuotes.Add("No quote found on this page.");
            }
            else
            {
                shortestQuotes.Add(shortestQuote);
            }
        }
        Console.WriteLine();

        return shortestQuotes;
    }

    public IEnumerable<string> GetShortestQuotesContainingParallel(string wordToSearch, IEnumerable<IEnumerable<string>> quotesCollection)
    {

        var shortestQuotes = new List<string>();

        var tasks = new List<Task>(quotesCollection.Count());
        foreach (var collection in quotesCollection)
        {
            var collectionCopy = collection;
            var task = Task.Run(() =>
            {
                string shortestQuote = GetShortestQuoteContaining(wordToSearch, collectionCopy);

                if (shortestQuote is null)
                {
                    shortestQuotes.Add("No quote found on this page.");
                }
                else
                {
                    shortestQuotes.Add(shortestQuote);
                }                
            });

            tasks.Add(task);            
        }

        Task.WaitAll(tasks.ToArray());
        Console.WriteLine();

        return shortestQuotes;
    }

    private static string GetShortestQuoteContaining(string wordToSearch, IEnumerable<string> collection)
    {
        string shortestQuote = null!;
        foreach (var quote in collection)
        {
            var index = 0;
            var textLength = quote.Length;

            var scannedString = new List<char>();
            while (index < textLength)
            {
                index = ScanString(quote, index, textLength, scannedString);

                if (IsMatch(wordToSearch, scannedString))
                {
                    if (shortestQuote is null)
                    {
                        shortestQuote = quote;
                    }
                    else if (quote.Length < shortestQuote.Length)
                    {
                        shortestQuote = quote;
                    }

                    break;
                }
                else
                {
                    scannedString.Clear();
                }

                index = SkipToNextWord(quote, index, textLength);
            }
        }

        return shortestQuote;
    }

    private static bool IsMatch(string wordToSearch, List<char> scannedString)
    {
        return new string(scannedString.ToArray()) == wordToSearch;
    }

    private static int ScanString(string quote, int index, int textLength, List<char> scannedString)
    {
        while (index < textLength
            && !char.IsWhiteSpace(quote[index])
            && quote[index] != ','
            && quote[index] != '.')
        {
            scannedString.Add(quote[index]);
            index++;
        }

        return index;
    }

    private static int SkipToNextWord(string quote, int index, int textLength)
    {
        while (index < textLength
                                && (char.IsWhiteSpace(quote[index])
                                || quote[index] == ','
                                || quote[index] == '.'))
        {
            index++;
        }

        return index;
    }

}
