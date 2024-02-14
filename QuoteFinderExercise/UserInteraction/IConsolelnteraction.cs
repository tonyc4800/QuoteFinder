using QuoteFinderExercise.App;

namespace QuoteFinderExercise.UserInteraction;

internal interface IConsolelnteraction
{
    void Show(string message);
    int ReadNumber(string message);
    string ReadSingleWord(string message);
}