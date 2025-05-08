namespace amybrowbackendAPI.DTOs
{
    public class LoginInput
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class AuthResult
    {
        public string Token { get; set; }
        public string Email { get; set; }
    }
}
