
using QuoteFinderExercise.App;
using QuoteFinderExercise.DataAccess;
using QuoteFinderExercise.DataProcessing;
using QuoteFinderExercise.UserInteraction;
try
{
    var quoteFinder =
        new QuoteFinderApp(
            new Consolelnteraction(),
            new QuoteProcessor(),
            new ApiReader());

    quoteFinder.Run();
}
catch (Exception ex)
{
    Console.WriteLine("Exception thrown: " + ex.Message);
}



Console.ReadKey();


