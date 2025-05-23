using AutoMapper;
using Devzteps_API.Application.DTO.Todo;
using Devzteps_API.Domain.Entities;

namespace Devzteps_API.Application.Mapping
{
    public class TodoMappingProfile : Profile
    {
        public TodoMappingProfile()
        {
            CreateMap<TodoItem, TodoCreateItemDTO>().ReverseMap();
            // ReverseMap cria mapeamento dos dois lados
        }
    }
}
