using QuoteFinderExercise.DataAccess;
using QuoteFinderExercise.DataProcessing;
using QuoteFinderExercise.UserInteraction;

namespace QuoteFinderExercise.App;
internal class QuoteFinderApp
{
    private IConsolelnteraction _consoleInteraction;
    private IQuoteProcessor _quoteProcessor;
    private IApiReader _apiReader;

    public QuoteFinderApp(IConsolelnteraction consoleInteraction, IQuoteProcessor quoteProcessor, IApiReader apiReader)
    {
        _consoleInteraction = consoleInteraction;
        _quoteProcessor = quoteProcessor;
        _apiReader = apiReader;
    }

    public async void Run()
    {

        var userInput = GetUserInput(_consoleInteraction);

        var quotesCollection = await _apiReader.ReadQuotes(userInput.QueryFilters);

        var shortestQuotes = userInput.ProcessParallel == "y"
            ? _quoteProcessor.GetShortestQuotesContainingParallel(userInput.WordToSearch, quotesCollection)
            : _quoteProcessor.GetShortestQuotesContaining(userInput.WordToSearch, quotesCollection);


        DisplayResults(shortestQuotes);
    }

    private void DisplayResults(IEnumerable<string> shortestQuotes)
    {
        _consoleInteraction.Show("");
        _consoleInteraction.Show(string.Join(Environment.NewLine + Environment.NewLine, shortestQuotes));
    }

    private static UserInput GetUserInput(IConsolelnteraction _consoleInteraction)
    {
        return new UserInput 
        {
            WordToSearch = _consoleInteraction.ReadSingleWord("What word are you looking for?"),
            QueryFilters = new QueryFilters()
            {
                NumberOfPages = _consoleInteraction.ReadNumber("How many pages do you want to check: "),
                QuoteLimit = _consoleInteraction.ReadNumber("How many quotes should be on each page that we are searching?: ")                
            },
            ProcessParallel = _consoleInteraction.ReadSingleWord("Process pages with multithreading? ('y' for yes, 'n' for no) ")
        };        
    }
}
