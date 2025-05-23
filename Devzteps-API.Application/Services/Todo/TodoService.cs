using Devzteps_API.Application.Interfaces.Todo;
using Devzteps_API.Domain.Entities;

namespace Devzteps_API.Application.Services.Todo
{
    public class TodoService : ITodoService
    {
        private readonly ITodoRepository _todoRepository;

        public TodoService(ITodoRepository todoRepository)
        {
            _todoRepository = todoRepository;
        }

        public async Task<IEnumerable<TodoItem>> GetAllAsync()
        {
            return await _todoRepository.GetAllAsync();
        }

        public async Task<TodoItem?> GetByIdAsync(int id)
        {
            return await _todoRepository.GetByIdAsync(id);
        }

        public async Task<TodoItem> CreateAsync(TodoItem item)
        {
            // Aqui você pode adicionar validações, regras de negócio etc
            return await _todoRepository.AddAsync(item);
        }
    }
}
