using Devzteps_API.Application.Interfaces.Todo;
using Devzteps_API.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Devzteps_API.Application.DTO.Todo;

namespace Devzteps_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TodoController : ControllerBase
    {
        private readonly ITodoService _todoService;
        private readonly IMapper _mapper;

        public TodoController(ITodoService todoService, IMapper mapper)
        {
            _todoService = todoService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var items = await _todoService.GetAllAsync();
            return Ok(items);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var item = await _todoService.GetByIdAsync(id);
            if (item == null)
                return NotFound();

            return Ok(item);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] TodoCreateItemDTO dto)
        {
            var entity = _mapper.Map<TodoItem>(dto);
            var created = await _todoService.CreateAsync(entity);

            var resultDto = _mapper.Map<TodoCreateItemDTO>(created);
            return CreatedAtAction(nameof(Get), new { id = resultDto.Id }, resultDto);
        }

    }
}
