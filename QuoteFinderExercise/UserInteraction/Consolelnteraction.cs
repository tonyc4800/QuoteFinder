namespace QuoteFinderExercise.UserInteraction;
internal class Consolelnteraction : IConsolelnteraction
{
    public void Show(string message) => Console.WriteLine(message);

    public int ReadNumber(string message)
    {
        int number;
        do
        {
            Console.WriteLine(message);
        }
        while (!int.TryParse(Console.ReadLine(), out number));

        return number;
    }

    public string ReadSingleWord(string message)
    {
        string input;
        do
        {
            Console.WriteLine(message);
            input = Console.ReadLine()!;
        }
        while (!IsSingleWord(input));
        return input;
    }

    private bool IsSingleWord(string input)
    {
        if(!string.IsNullOrWhiteSpace(input) 
            && IsNotMultipleWords(input)
            && HasNoNumbers(input))
        {
            return true;
        }
        return false;
    }

    private bool HasNoNumbers(string input)
    {
        return !input.Any(char.IsDigit);
    }

    private bool IsNotMultipleWords(string input)
    {
        return !input.Trim().Contains(" ");        
    }

}
