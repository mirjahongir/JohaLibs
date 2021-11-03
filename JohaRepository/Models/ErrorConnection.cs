namespace JohaRepository.Models
{
    public class ErrorConnection
    {
        private ErrorConnection() { }
        public ErrorConnection(string loginName, string password, string url = null, string dbName = null)
        {
            LoginName = loginName;
            Password = password;
            Url = url ?? "";
            DbName = dbName ?? "ErrorDb.db";
        }
        public string LoginName { get; set; }
        public string Password { get; set; }
        public string Url { get; set; }
        public string DbName { get; set; }
        public bool CHeck()
        {
            if (!string.IsNullOrEmpty(LoginName) &&
                string.IsNullOrEmpty(Password) &&
                string.IsNullOrEmpty(Url)
                )
                return true;
            return false;


        }

    }
}