using API.Enums;

namespace API.DTOs
{
    public class UserDTO
    {
        public System.Guid Id { get; set; }
        public required string Name { get; set; }
        public AuthLevel UserAuthLevel { get; set; }
    }
}
