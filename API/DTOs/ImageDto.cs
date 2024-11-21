namespace API.DTOs
{
    public class ImageDto
    {
        public System.Guid Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }

        public string? Author { get; set; }
    }
}
