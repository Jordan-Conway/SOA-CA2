namespace API.Models
{
    public class ImageItem
    {
        public System.Guid Id { get; set; }
        public string? Name { get; set; }
        public required string Url { get; set; }
        public string? Author { get; set; }
    }
}
