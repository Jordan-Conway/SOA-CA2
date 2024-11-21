using API.Enums;

namespace API.Models
{
    public class UserItem
    {
        public System.Guid Id { get; set; }
        public required string Name { get; set; }
        public required string PasswordHash { get; set; }

        public AuthLevel UserAuthLevel { get; set; }
    }
}
