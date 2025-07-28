using Microsoft.AspNetCore.Mvc;
using TesteLocalize.Application.UseCases;
using TesteLocalize.WebAPI.Models;

namespace TesteLocalize.WebAPI.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly RegisterUserUseCase _registerUserUseCase;
        private readonly AuthenticateUserUseCase _authenticateUserUseCase;

        public AuthController(RegisterUserUseCase registerUserUseCase, AuthenticateUserUseCase authenticateUserUseCase)
        {
            _registerUserUseCase = registerUserUseCase;
            _authenticateUserUseCase = authenticateUserUseCase;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register (UserRegisterRequest request)
        {
            try
            {
                var user = await _registerUserUseCase.ExecuteAsync(request.Name, request.Email, request.Password);

                return CreatedAtAction(nameof(Register), new { id = user.Id }, new {user.Id, user.Name, user.Email});
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(UserLoginRequest request)
        {
            try
            {
                var token = await _authenticateUserUseCase.ExecuteAsync(request.Email, request.Password);
                return Ok(new { Token = token });
            }
            catch (Exception ex)
            {
                return Unauthorized(new { message = ex.Message });
            }
        }
    }
}
