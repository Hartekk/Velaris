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

        private string GetUserIdFromToken()
        {
            var identity = User.Identity as ClaimsIdentity;
            return identity?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        }


        [HttpPost, Route("create")]
        
        public IHttpActionResult Create(ToDoItemAddReq request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userId = GetUserIdFromToken();

            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized();
            }

            var todo = _todoService.Create(userId, request);
            return Ok(todo);
        }

        [HttpPut, Route("{id:int}")]
        public IHttpActionResult Edit(int id, ToDoItemEditReq request) {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userId = GetUserIdFromToken();
            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized();
            }

            request.Id = id;

            var updatedToDo = _todoService.Edit(userId, request);

            return Ok(updatedToDo);
        }

        [HttpDelete, Route("delete/{id:int}")]
        public IHttpActionResult Delete(int id)
        {
            var userId = GetUserIdFromToken();
            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized();
            }

            _todoService.Delete(userId, id);

            return StatusCode(System.Net.HttpStatusCode.NoContent);
        }

        [HttpGet, Route("getall")]
        public IHttpActionResult GetAll() {
            var userId = GetUserIdFromToken();
            if (!string.IsNullOrEmpty(userId))
            {
                return Unauthorized();
            }

            var todos = _todoService.GetAll(userId);

            return Ok(todos);
        }

        [HttpGet, Route("get{id:int}")]
        public IHttpActionResult Get(int id)
        {
            var userId = GetUserIdFromToken();
            if (!string.IsNullOrEmpty(userId))
            {
                return Unauthorized();
            }

            var todo = _todoService.GetOneTask(userId, id);

            return Ok(todo);
        }

        [HttpDelete, Route("deleteall")]
        public IHttpActionResult DeleteAll()
        {
            var userId = GetUserIdFromToken();
            if (string.IsNullOrEmpty(userId)) {
                return Unauthorized();
                    }
            _todoService.DeleteAll(userId);

            return StatusCode(System.Net.HttpStatusCode.NoContent);
        }
    }
}