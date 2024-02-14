namespace QuoteFinderExercise.DTO;

public record Root 
(
    int statusCode,
    string message,
    Pagination pagination,
    int totalQuotes,
    IReadOnlyList<Datum> data
);


