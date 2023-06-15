using GitUsers.API.Models;
using GitUsers.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace GitUsers.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RetriveUsers : ControllerBase
    {
        private readonly IUsersService _usersService;

        public RetriveUsers(IUsersService usersService)
        {
            this._usersService = usersService;
        }

        [HttpGet(Name = "RetrieveUsers")]
        public async Task<IActionResult> Get([ModelBinder(typeof(ModelBinderUsers))] List<string> usernames)
        {
            
            var users = await _usersService.RetrieveUsers(usernames);

            if (users.Any())
            {
                return Ok(users);
            }
            return NotFound();
        }

        
    }
}