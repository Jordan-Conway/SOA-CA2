namespace api.Models;

public class QuestionDTO
{
    public int Id { get; set; }
    public string? Question { get; set; }
    public string? CreatedBy { get; set; }

    public QuestionDTO(int id, string question, string createdBy)
    {

    }
}