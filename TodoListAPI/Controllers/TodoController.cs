using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using TodoListAPI.Entities;
using TodoListAPI.Models;
using TodoListAPI.Services;

namespace TodoListAPI.Controllers
{
    // Controller class which is responsible for communication between frontend 
    // and api via the HTTP protocol
    [ApiController]
    [Route("[controller]")]
    public class TodoController : ControllerBase
    {
        private readonly ITodoService _todoService;

        // Dependency incjection of service where are the methods with 
        // buisness logic
        public TodoController(ITodoService todoService)
        {
            _todoService = todoService;
        }


        // This method gets all entities from service.
        [HttpGet]
        public ActionResult<IEnumerable<Todo>> GetAll()
        {
            var todoListDto = _todoService.GetAll();
            return Ok(todoListDto);
        }

        // This method gets specific entity by ID from service.
        // ID is sending to api by the route.
        [HttpGet("{id}")]
        public ActionResult<Todo> Get([FromRoute] int id)
        {
            var todo = _todoService.GetById(id);

            if (todo is null)
            {
                return NotFound("ToDo not found");
            }
            else
            {
                return Ok(todo);
            }
        }

        // This method gest data of new entity and sends it to service.
        // Data is sent by the body
        [HttpPost]
        public ActionResult CreateToDo([FromBody] CreateTodoDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var created = _todoService.Create(dto);

            if (!created)
            {
                return BadRequest("The date of expire is invalid");
            }
            return Created(" ", null);
        }

        // This method sends request to service of delete specific entity by ID.
        // ID is sending to api by the route.
        [HttpDelete("{id}")]
        public ActionResult Delete([FromRoute] int id)
        {
            var isDeleted = _todoService.Delete(id);

            if (isDeleted)
            {
                return NoContent();
            }
            return NotFound();
        }

        // This method sends ID and new data to the service of specific entity. 
        // ID is sending to api by the route.
        // Data are sending to api by the body.
        [HttpPut("{id}")]
        public ActionResult Update([FromRoute] int id, [FromBody] UpdateTodoDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var isUpdated = _todoService.Update(id, dto);
            if (!isUpdated)
            {
                return NotFound();
            }
            return Ok();
        }

        // This method sends request to the servis with
        // change done column in specyific entity by ID.
        // ID is sending to api by the route - ".../done/{id}".
        [HttpPost("done/{id}")]
        public ActionResult SetAsDoneUndone([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var isChanged = _todoService.SetAsDoneUndone(id);
            if (!isChanged)
            {
                return NotFound();
            }
            return Ok();
        }

        // This method validate and sends to service percent value in specific entity by ID.
        // The Percent value have to be sended by the body.
        // Entity's ID by the route - ".../percent/{id}".
        [HttpPut("percent/{id}")]
        public ActionResult SetPercent([FromRoute] int id, [FromBody] SetPercentDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            else if (dto.PercentComplete > 100 || dto.PercentComplete < 0)
            {
                return BadRequest("The value is invalid");
            }
            var isChanged = _todoService.SetPercent(id, dto);
            if (!isChanged)
            {
                return NotFound();
            }
            return Ok();
        }

        // This method gets specific entity by date from database.
        // By the number sent by the route, number specify the searching.
        // 0 - today
        // 1 - next day
        // 2 - current week
        // 3 - current month
        // 4- current year
        [HttpGet("date/{number}")]
        public ActionResult<IEnumerable<Todo>> GetByDate([FromRoute] int number)
        {
            var todoListDto = _todoService.GetByDate(number);
            return Ok(todoListDto);
        }
    }

}
