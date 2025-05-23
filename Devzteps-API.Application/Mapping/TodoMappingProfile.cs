using AutoMapper;
using Devzteps_API.Application.DTO.Todo;
using Devzteps_API.Domain.Entities;

namespace Devzteps_API.Application.Mapping
{
    public class TodoMappingProfile : Profile
    {
        public TodoMappingProfile()
        {
            // Mapeamento bidirecional para criação (DTO <-> Entidade)
            CreateMap<TodoItem, TodoCreateItemDTO>().ReverseMap();

            // Mapear leitura (Entidade -> DTO)
            CreateMap<TodoItem, TodoReadItemDTO>();
        }
    }
}
