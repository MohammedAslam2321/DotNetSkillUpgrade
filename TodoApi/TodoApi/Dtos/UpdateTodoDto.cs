using System.ComponentModel.DataAnnotations;
using TodoApi.Validations;

namespace TodoApi.Dtos;

public class UpdateTodoDto : CreateTodoDto
{
    public int Id { get; set; }

    [Required(ErrorMessage ="Is completed required")]
    public bool IsCompleted { get; set; }
}
