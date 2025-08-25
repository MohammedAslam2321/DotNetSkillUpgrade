using TodoApi.Dtos;
using TodoApi.Entities;

namespace TodoApi.Mapping;

public static class TodoMapping
{
    public static TodoDto ToTodoDto(this Todo model)
    {
        return new TodoDto
        {
            Id = model.Id,
            Title = model.Title,
            Description = model.Description,
            IsCompleted = model.IsCompleted,
            DueDate = model.DueDate
        };
    }

    public static Todo ToTodo(this CreateTodoDto dto)
    {
        return new Todo
        {
            Title = dto.Title,
            Description = dto.Description,
            IsCompleted = false,
            DueDate = dto.DueDate
        };
    }
}
