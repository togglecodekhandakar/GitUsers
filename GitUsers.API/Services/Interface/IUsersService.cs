using GitUsers.API.Models;

namespace GitUsers.API.Services.Interface
{
    public interface IUsersService
    {
        public Task<List<User>> RetrieveUsers(List<string> usernames);

    }
}
