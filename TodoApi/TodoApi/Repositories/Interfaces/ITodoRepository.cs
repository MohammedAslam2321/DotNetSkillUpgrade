using TodoApi.Dtos;
using TodoApi.Entities;
using TodoApi.Helpers;

namespace TodoApi.Repositories.Interfaces;

public interface ITodoRepository
{
    Task<IEnumerable<TodoDto>> GetAllTodosAsync(QueryObject query);

    Task<TodoDto?> GetTodoByIdAsync(int id);

    Task<TodoDto> CreateTodoAsync(Todo entity);

    Task<Todo?> FindTodoByIdAsync(int id);

    Task UpdateTodoAsync(Todo entity);

    Task DeleteTodoAsync(Todo entity);
}
