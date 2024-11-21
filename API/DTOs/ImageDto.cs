namespace API.DTOs
{
    public class ImageDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }

        public string? Author { get; set; }
    }
}
