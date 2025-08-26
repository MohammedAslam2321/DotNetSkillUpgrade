using System.ComponentModel.DataAnnotations;
using TodoApi.Validations;

namespace TodoApi.Dtos;

public class UpdateTodoDto : CreateTodoDto
{
    public int Id { get; set; }

    public bool IsCompleted { get; set; }
}
