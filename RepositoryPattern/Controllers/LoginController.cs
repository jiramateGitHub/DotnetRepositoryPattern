using Microsoft.AspNetCore.Mvc;
using RepositoryPattern.Models;
using RepositoryPattern.Repository;
using static RepositoryPattern.Models.Jwt;

namespace RepositoryPattern.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly GenToken _genToken = new GenToken();

        [HttpPost("GetToken")]
        public IActionResult onPost_UserLogin([FromBody] UserAuthen request)
        {
            ResponseMessage<Object> response = new ResponseMessage<Object>();
            try
            {
                UserLogin User = new UserLogin();
                User.Username = request.Username;
                var TT = User;
                if (User.Username == "admin")
                {
                    response.body = TT;
                    response.Status = true;
                    response.Token = _genToken.GenerateJSONWebToken(100, User);
                    return Ok(response);

                }
                else
                {
                    response.Status = false;
                    response.ErrorMessage = "Permission denied";
                    return BadRequest(response);
                }
            }
            catch (Exception ex)
            {
                response.Status = false;
                response.ErrorMessage = ex.Message;
                return BadRequest(response);
            }
        }
    }
}
