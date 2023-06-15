namespace GitUsers.API.Config
{
    public class GitApiOptions
    {
        public string Endpoint { get; set; }
        public string Token { get; set; }
        public int  MaxDegreeOfParallelism { get; set; }
    }
}
