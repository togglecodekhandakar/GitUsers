using GitUsers.API.Models;

namespace GitUsers.API.Services.BLL
{
    public static class DataHelper
    {

        public static User CalculateAvgFollowers(User user)
        {
            if (user.public_repos != 0)
            {
                user.avgFollowers = user.followers / user.public_repos;
            }
            else
            {
                user.avgFollowers = 0.0M; // M tells that 0.0 is a decimal
            }
            return user;
        }

        public static List<User> SortUserListAlphabetically(List<User> users)
        {
            var usersInOrder = users.OrderBy(u => u.name).ToList();
            return usersInOrder;
        }

        public static List<string> RemoveDuplicateEntry(List<string> usernames)
        {
            return usernames.Distinct().ToList();
        }

        public static User NotFoundUser()
        {
            return new User()
            {
                id = 0,
                name = string.Empty,
                login = string.Empty,
                company = string.Empty,
                public_repos = 0,
                followers = 0,
                avgFollowers = 0.0M,
                url = "No url"
            };
        }
    }
}
