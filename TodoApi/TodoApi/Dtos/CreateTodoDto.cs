using System.ComponentModel.DataAnnotations;
using TodoApi.Validations;

namespace TodoApi.Dtos;

public class CreateTodoDto
{
    [Required(ErrorMessage ="Title is required")]
    [MaxLength(50,ErrorMessage ="Title must be at most 50 characters only")]
    public string Title { get; set; } = string.Empty;

    [MaxLength(1000, ErrorMessage = "Description must be at most 1000 characters only")]
    public string Description { get; set; } = string.Empty;

    [Required(ErrorMessage ="DueDate is required")]
    [FutureOrTodayDateAttribute(ErrorMessage ="Due Date must be today or future date")]
    public DateTime DueDate { get; set; }
}
