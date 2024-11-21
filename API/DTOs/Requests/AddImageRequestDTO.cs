namespace API.DTOs.Requests
{
    public class AddImageRequestDTO
    {
        public required System.Guid UserID { get; set; }
        public required string Name { get; set; }
        public required string Url { get; set; }
    }
}
