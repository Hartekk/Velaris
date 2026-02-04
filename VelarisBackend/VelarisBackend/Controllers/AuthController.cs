using System;
using System.Web.Http;
using VelarisBackend.DTOs;
using VelarisBackend.Repositories;
using VelarisBackend.Services;


namespace VelarisBackend.Controllers
{
    [RoutePrefix("api/auth")]
    public class AuthController : ApiController
    {
        private readonly IAuthService _authService;
        private readonly IAuthTokenRepository _authTokenRepository;

        public AuthController(IAuthService authService, IAuthTokenRepository authTokenRepository)
        {
            _authService = authService;
            _authTokenRepository = authTokenRepository;
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
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }

        [HttpPost]
        [Route("logout")]
        public IHttpActionResult Logout()
        {
            var authHeader = Request.Headers.Authorization;

            if (authHeader == null || authHeader.Scheme != "Bearer")
            {
                return BadRequest("Missing token");
            }

            var token = authHeader.Parameter;

            var storedToken = _authTokenRepository.Get(token); ;
            if (storedToken == null)
            {
                return Unauthorized();
            }

            _authTokenRepository.Remove(storedToken);
            _authTokenRepository.SaveChanges();

            return Ok();
        }
    }
}
