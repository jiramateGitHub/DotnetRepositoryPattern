using Azure.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RepositoryPattern.Models;
using RepositoryPattern.Repository;

namespace RepositoryPattern.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpPost("User")]
        public IActionResult onPost_User([FromBody] RequestMessage<User> request)
        {
            if (request != null)
            {
                return Ok(_userRepository.User(request));
            }
            else
            {
                return BadRequest(new ResponseMessage<int>() { Status = false, ErrorMessage = "Access denied" });
            }
        }
    }
}
