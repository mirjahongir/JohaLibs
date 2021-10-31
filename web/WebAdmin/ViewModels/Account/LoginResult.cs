namespace WebAdmin.ViewModels.Account
{
    public class LoginResult
    {
        public bool Success { get; set; }
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public int HttpStatus { get; set; }
        public string Error { get; set; }
    }
}
