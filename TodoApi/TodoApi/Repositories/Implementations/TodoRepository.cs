using Microsoft.EntityFrameworkCore;
using TodoApi.Data;
using TodoApi.Dtos;
using TodoApi.Entities;
using TodoApi.Helpers;
using TodoApi.Mapping;
using TodoApi.Repositories.Interfaces;

namespace TodoApi.Repositories.Implementations;

public class TodoRepository : ITodoRepository
{
    private readonly AppDbContext _context;

    public TodoRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<TodoDto> CreateTodoAsync(Todo entity)
    {
        await _context.Todos.AddAsync(entity);
        await _context.SaveChangesAsync();
        return entity.ToTodoDto();
    }

    public async Task<IEnumerable<TodoDto>> GetAllTodosAsync(QueryObject query)
    {
        var todoQuery = _context.Todos.AsNoTracking().AsQueryable();

        if (!string.IsNullOrWhiteSpace(query.Title))
        {
            todoQuery = todoQuery.Where(t => t.Title.Contains(query.Title));
        }

        if (!string.IsNullOrWhiteSpace(query.SortBy))
        {
            if (query.SortBy.Equals("duedate", StringComparison.OrdinalIgnoreCase))
            {
                todoQuery = query.IsDescending ? todoQuery.OrderByDescending(t => t.DueDate) : todoQuery.OrderBy(t => t.DueDate);
            }
        }

        var skipNumber = (query.PageNumber - 1) * query.PageSize;
        todoQuery = todoQuery.Skip(skipNumber).Take(query.PageSize);

        var todos = await todoQuery.ToListAsync();

        return todos.Select(todo => todo.ToTodoDto());
    }

    public async Task<TodoDto?> GetTodoByIdAsync(int id)
    {
        var todo = await _context.Todos.AsNoTracking().Where(t => t.Id == id).FirstOrDefaultAsync();
        if (todo is null) return null;

        return todo.ToTodoDto();

    }


    public async Task<Todo?> FindTodoByIdAsync(int id)
    {
        return await _context.Todos.FindAsync(id);
    }

    public async Task UpdateTodoAsync(Todo entity)
    {
        _context.Update(entity);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteTodoAsync(Todo entity)
    {
        _context.Remove(entity);
        await _context.SaveChangesAsync();

    }
}
