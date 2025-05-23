using Devzteps_API.Domain.Entities;

namespace Devzteps_API.Application.Interfaces.Todo
{
    public interface ITodoService
    {
        Task<IEnumerable<TodoItem>> GetAllAsync();
        Task<TodoItem?> GetByIdAsync(int id);
        Task<TodoItem> CreateAsync(TodoItem item);
        // Outros métodos como UpdateAsync, DeleteAsync podem ser adicionados aqui
    }
}

