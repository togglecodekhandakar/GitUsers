namespace GitUsers.API.Models
{
    public class User
    {

        public int id { get; set; }
        public string url { get; set; }
        public string name { get; set; }
        public string login { get; set; }

        public string company { get; set; }

        public int followers { get; set; }

        public int public_repos { get; set; }

       public int avgFollowers { get; set; }
    }
}
