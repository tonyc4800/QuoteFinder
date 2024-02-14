namespace QuoteFinderExercise.App;
internal readonly struct UserInput
{
    public string WordToSearch { get; init; }
    public QueryFilters QueryFilters { get; init; }
    public string ProcessParallel {  get; init; }
}
