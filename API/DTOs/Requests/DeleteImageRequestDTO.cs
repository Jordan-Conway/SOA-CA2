namespace API.DTOs.Requests
{
    public class DeleteImageRequestDTO
    {
        public required System.Guid UserID { get; set; }
        public required System.Guid ImageID { get; set; }
    }
}
