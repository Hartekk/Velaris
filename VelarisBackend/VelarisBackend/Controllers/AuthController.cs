using System.Web.Http;
using VelarisBackend.DTOs;
using VelarisBackend.Services;

namespace VelarisBackend.Controllers
{
    [RoutePrefix("api/auth")]
    public class AuthController : ApiController
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost]
        [Route("register")]
        public IHttpActionResult Register(RegisterReq request)
        {
            try
            {
                _authService.Register(
                    request.Username,
                    request.Password,
                    request.Email
                );

                return StatusCode(System.Net.HttpStatusCode.Created);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("login")]
        public IHttpActionResult Login(LoginReq request)
        {
            try
            {
                var token = _authService.Login(request.Username, request.Password);
                return Ok(new { token });
            }
            catch
            {
                return Unauthorized();
            }
        }
    }
}
