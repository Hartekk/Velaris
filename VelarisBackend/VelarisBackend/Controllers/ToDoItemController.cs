using System;
using System.Security.Claims;
using System.Web.Http;
using VelarisBackend.DTOs;
using VelarisBackend.Services;

namespace VelarisBackend.Controllers
{
    [Authorize, RoutePrefix("api/todoitem")] // ensure JWT is present to create items & set route base
    public class ToDoItemController : ApiController
    {
        private readonly IToDoItemService _todoService;

        public ToDoItemController(IToDoItemService todoService)
        {
            _todoService = todoService;
        }


        [HttpPost, Route("")]
        
        public IHttpActionResult Create(ToDoItemAddReq request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var identity = (ClaimsIdentity)User.Identity;
            var userId = identity.FindFirst(ClaimTypes
                .NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized();
            }

            var todo = _todoService.Create(userId, request);
            return Ok(todo);
        }
      

    }
}