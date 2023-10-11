using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using TodoApp.Core.Entities.Business;
using TodoApp.Core.Interfaces.IServices;
using UserApp.Core.Interfaces.IServices;

namespace TodoApp.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly ILogger<ToDoController> _logger;
        private readonly IUserService _userService;
        public AuthController(ILogger<ToDoController> logger, IUserService userService)
        {
            _logger = logger;
            _userService = userService;
        }

        /// <summary>
        /// Authenticate Login Info and return access token
        /// </summary>
        /// <param name="authenticateRequest"></param>
        /// <returns></returns>
        [SwaggerOperation(Summary = "Authenticate Login Info and return access token")]
        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> AuthenticateAsync(AuthenticateRequest authenticateRequest)
        {
            var response = await _userService.Authenticate(authenticateRequest);

            if (response == null)
                return BadRequest(new { message = "Username or password is incorrect" });
                

            return Ok(response);
        }
    }
}
