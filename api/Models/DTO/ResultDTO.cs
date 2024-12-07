namespace api.Models;

public class ResultDTO
{
    public string? Question { get; set; }
    public List<ResultItem> Results { get; set; } = [];

    public ResultDTO(string question, List<ResultItem> results)
    {
        this.Question = question;
        this.Results = results;
    }
}