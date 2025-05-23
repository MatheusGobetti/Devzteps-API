using Devzteps_API.Domain.Entities;

namespace Devzteps_API.Application.Interfaces.Todo
{
    public interface ITodoRepository
    {
        Task<IEnumerable<TodoItem>> GetAllAsync();
        Task<TodoItem?> GetByIdAsync(int id);
        Task<TodoItem> AddAsync(TodoItem item);
    }
}
