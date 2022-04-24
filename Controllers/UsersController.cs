using MarieSundbergAssignment.Models.User;
using MarieSundbergAssignment.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MarieSundbergAssignment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _service;

        public UsersController(IUserService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct(UserModel request)
        {
            var item = await _service.CreateUserAsync(request);
            if (item != null)
            {
                return new OkObjectResult(item);
            }

            return new BadRequestResult();
        }
        
        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            return new OkObjectResult(await _service.GetAllUsersAsync());
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, UserModel request)
        {
            var item = await _service.UpdateUserAsync(id, request);
            if (item != null)
            {
                return new OkObjectResult(item);
            }

            return new BadRequestResult();

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            if (await _service.DeleteUserAsync(id))
            {
                return new OkResult();
            }

            return new BadRequestResult();
        }
    }
}
