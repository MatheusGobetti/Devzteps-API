using Devzteps_API.Application.Interfaces.Todo;
using Devzteps_API.Domain.Entities;

namespace Devzteps_API.Application.Services.Todo
{
    public class TodoService : ITodoService
    {
        private readonly ITodoRepository _repository;

        public TodoService(ITodoRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<TodoItem>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<TodoItem?> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<TodoItem> CreateAsync(TodoItem item)
        {
            // Aqui você pode adicionar validações, regras de negócio etc
            return await _repository.AddAsync(item);
        }
    }
}
