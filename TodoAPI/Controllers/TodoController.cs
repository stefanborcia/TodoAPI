using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TodoAPI.Contracts;
using TodoAPI.Interface;

namespace TodoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoController : ControllerBase
    {
        private readonly ITodoServices _todoServices;

        public TodoController(ITodoServices todoServices)
        {
            _todoServices = todoServices;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            try
            {
                var todo = await _todoServices.GetAllAsync();
                if (todo is null || !todo.Any())
                {
                    return Ok(new { message = "No Todo items found" });
                }
                return Ok(new { message = "Successfully retrieved Todo items", data = todo });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateTodoAsync(CreateTodoRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { message = "Invalid request" });
            }

            try
            {
                await _todoServices.CreateTodoAsync(request);
                return Ok(new { message = "Todo item created successfully" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateTodoAsync(Guid id, UpdateTodoRequest request)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var todo = await _todoServices.GetByIdAsync(id);
                if(todo is null )
                {
                    return NotFound(new { message = $"Todo item with id: {id} not found" });
                }

                await _todoServices.UpdateTodoAsync(id, request);
                return Ok(new { message = $"Todo item with id: {id} updated successfully" });
            }catch(Exception ex)
            {
                return StatusCode(500, new {message = $"An error occurred while updating blog post with id {id}", error = ex.Message });
            }
        }
    }
}
