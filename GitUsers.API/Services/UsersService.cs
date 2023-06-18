using System.Net.Http.Headers;
using System.Collections.Concurrent;
using Microsoft.Extensions.Options;
using System.Net.Http.Json;
using GitUsers.API.Models;
using GitUsers.API.Config;
using GitUsers.API.Services.Interface;
using GitUsers.API.Services.BLL;

namespace GitUsers.API.Services
{
    

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
                    return DataHelper.NotFoundUser();
                }
                var responseContent = usersResponse.Content;
                try
                {
                    var userRaw = await responseContent.ReadFromJsonAsync<User>();
                    var userUpdated = DataHelper.CalculateAvgFollowers(userRaw);
                    return userUpdated;
                }
                catch (Exception)
                {

                    return DataHelper.NotFoundUser();
                }
                

                

                
            }
        }
        public async Task<List<User>> RetrieveUsers(List<string> usernames)
        {
            var usernamesDistinct = DataHelper.RemoveDuplicateEntry(usernames); 
            var parallellOption = new ParallelOptions { MaxDegreeOfParallelism = _apiConfig.MaxDegreeOfParallelism };

            //List<User> userDetails = new List<User>();    
            ConcurrentBag<User> userDetails = new ();
             Parallel.ForEach(usernamesDistinct, parallellOption,  username =>
            {
                userDetails.Add( RetriveIndividualUser(username).GetAwaiter().GetResult());
            });

            return DataHelper.SortUserListAlphabetically(userDetails.ToList());
        }
    }
}
