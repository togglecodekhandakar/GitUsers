using GitUsers.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace GitUsers.UnitTests.Fixtures
{
    public static class UsersFixtures
    {
        public static List<User> GetTestUsers() =>
            new()
            {
                new User
                {
                    
                    id = 1,
                    url = "testurl1",
                    name = "Test1",
                    login = "TestUser1",
                    company = "TestCompany1",
                    followers = 1,
                    public_repos = 1,
                    avgFollowers = 1.0M

                },
                new User
                {
                    id = 2,
                    url = "testurl2",
                    name = "Test2",
                    login = "TestUser2",
                    company = "TestCompany2",
                    followers = 2,
                    public_repos = 2,
                    avgFollowers = 2.0M
                },
                new User
                {
                    id = 3,
                    url = "testurl3",
                    name = "Test3",
                    login = "TestUser3",
                    company = "TestCompany3",
                    followers = 3,
                    public_repos = 3,
                    avgFollowers = 3.0M
                },
                new User
                {
                    id = 4,
                    url = "testurl4",
                    name = "Test4",
                    login = "TestUser4",
                    company = "TestCompany4",
                    followers = 4,
                    public_repos = 4,
                    avgFollowers = 4.0M
                },

            };

        public static List<string> GetTestUsernames() =>
            new() { "zhuangjiaju", "dangdang01234", "chenxian01", 
                "jipengfei-jpf", "rullzer","MorrisJobke",
                "nickvergessen","ameuleman",
            "moodysalem", "haydenadams"};
    }
}
