namespace api.Models;

public class QuestionEntry
{
    public int Id { get; set; }
    public string? Question { get; set; }
    public string? CreatedBy { get; set; }

    public QuestionEntry(int id, string question, string createdBy)
    {
        this.Id = id;
        this.Question = question;
        this.CreatedBy = createdBy;
    }
}