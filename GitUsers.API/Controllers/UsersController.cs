using Microsoft.AspNetCore.Mvc;

namespace GitUsers.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {


        public UsersController()
        {
  
        }

        [HttpGet(Name = "GetUsers")]
        public async Task<IActionResult> Get()
        {
            return null;
        }
    }
}