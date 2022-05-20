using GlonasssoftTestWebApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace GlonasssoftTestWebApi.Controllers
{
    [Route("users")]
    [ApiController]
    public class UsersController:Controller
    {

        private readonly UsersService usersService;

        public UsersController(UsersService usersService)
        {
            this.usersService = usersService;
        }

        [HttpPost("create")]
        public async Task CreateUser()
        {
            await usersService.CreateUserAsync();
        }
    }
}
