using System.Web.Http;
using VelarisBackend.Repositories;

namespace VelarisBackend.Controllers
{
    [RoutePrefix("api/user")]
    public class UserController : ApiController
    {
        private readonly Repositories.IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpGet]
        [Route("{username}")]
        public IHttpActionResult GetUserByUserName(string username)
        {
            var user = _userRepository.GetByUserName(username);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }
    }
}