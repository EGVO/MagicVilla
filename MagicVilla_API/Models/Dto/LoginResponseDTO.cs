namespace MagicVilla_API.Models.Dto
{
    public class LoginResponseDTO
    {
        public UserDto User { get; set; }

        public string Token { get; set; }

        public string Role { get; set; }
     }
}
