using System.Net.Http.Headers;
using System.Collections.Concurrent;
using Microsoft.Extensions.Options;
using System.Net.Http.Json;

namespace GitUsers.Services
{
    public interface IUsersService
    {
        public Task<List<User>> RetrieveUsers(List<string> usernames);

       

    }

    public class UsersService : IUsersService
    {
        private readonly HttpClient _httpClient;
        private readonly GitApiOptions _apiConfig;

        public UsersService(HttpClient httpClient, 
            IOptions<GitApiOptions>  apiConfig) 
        { 
            _httpClient = httpClient;
            _apiConfig = apiConfig.Value;
        }

        private async Task<User> RetriveIndividualUser(string username)
        {
            var finalEndpoint = _apiConfig.Endpoint + username;
            using (var requestMessage =
            new HttpRequestMessage(HttpMethod.Get, finalEndpoint))
            {
                requestMessage.Headers.UserAgent.TryParseAdd("request");
                requestMessage.Headers.Authorization =
                    new AuthenticationHeaderValue("Bearer", _apiConfig.Token);

                var usersResponse = await _httpClient.SendAsync(requestMessage);

                //var usersResponse = await _httpClient.GetAsync(_apiConfig.Endpoint);

                if (usersResponse.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    return new User();
                }
                var responseContent = usersResponse.Content;
                var user1 = await responseContent.ReadFromJsonAsync<User>();



                Thread.Sleep(1000); // Do I need it?? try again without
                return user1;
            }
        }
        public async Task<List<User>> RetrieveUsers(List<string> usernames)
        {
            
            var parallellOption = new ParallelOptions { MaxDegreeOfParallelism = _apiConfig.MaxDegreeOfParallelism };

            //List<User> userDetails = new List<User>();    
            ConcurrentBag<User> userDetails = new ();
             Parallel.ForEach(usernames, parallellOption,  username =>
            {
                userDetails.Add( RetriveIndividualUser(username).GetAwaiter().GetResult());
            });

            return userDetails.ToList();
        }
    }
}
