
using System.Web.Http;


namespace VelarisBackend.Controllers
{
    public class TestController : ApiController
    {
        private readonly Services.ITestService _testService;

        public TestController(Services.ITestService testService)
        {
            _testService = testService;
        }

        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/test/ping")]
        public IHttpActionResult Ping()
        {
            var response = _testService.Ping();
            return Ok(response);
        }
    }
}