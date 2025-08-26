using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TodoApi.Dtos;
using TodoApi.Helpers;
using TodoApi.Mapping;
using TodoApi.Repositories.Interfaces;

namespace TodoApi.Controllers;
[Route("api/[controller]")]
[ApiController]
public class TodosController : ControllerBase
{
    private readonly ITodoRepository _todoRepository;

    public TodosController(ITodoRepository todoRepository)
    {
        _todoRepository = todoRepository;
    }


    [HttpGet]
    public async Task<ActionResult<IEnumerable<TodoDto>>> GetTodos([FromQuery] QueryObject query)
    {
        return Ok( await _todoRepository.GetAllTodosAsync(query));
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<TodoDto>> GetTodo([FromRoute] int id)
    {
        var todo = await _todoRepository.GetTodoByIdAsync(id);
        if (todo is null) return NotFound("Todo not found");

        return Ok(todo);
    }

    [HttpPost]
    public async Task<ActionResult<TodoDto>> CreateTodo([FromBody] CreateTodoDto dto)
    {
        var todo = dto.ToTodo();
        var todoDto = await _todoRepository.CreateTodoAsync(todo);
        return CreatedAtAction(nameof(GetTodo), new { id = todoDto.Id }, todoDto);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateTodo([FromRoute] int id, [FromBody] UpdateTodoDto dto)
    {
        if (id != dto.Id) return BadRequest("The id in route and body is not same");

        var existingTodo = await _todoRepository.FindTodoByIdAsync(id);

        if (existingTodo is null) return NotFound("Todo not found");

        existingTodo.Title = dto.Title;
        existingTodo.Description = dto.Description;
        existingTodo.DueDate =dto.DueDate;
        existingTodo.IsCompleted = dto.IsCompleted;

        await _todoRepository.UpdateTodoAsync(existingTodo);

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTodo([FromRoute] int id)
    {
        var existingTodo = await _todoRepository.FindTodoByIdAsync(id);

        if(existingTodo is null) return NotFound("Todo not found");

        await _todoRepository.DeleteTodoAsync(existingTodo);

        return NoContent();
    }
}
