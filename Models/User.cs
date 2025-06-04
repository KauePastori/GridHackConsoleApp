namespace GridHackConsoleApp.Models
{
    public class User
    {
        public string Username { get; }
        private string Password { get; }
        public bool IsLoggedIn { get; private set; } = false;

        public User(string username, string password)
        {
            Username = username;
            Password = password;
        }

        public bool Login(string username, string password)
        {
            if (username == Username && password == Password)
            {
                IsLoggedIn = true;
                return true;
            }
            return false;
        }
    }
}
