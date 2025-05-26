using AutoMapper;
using Devzteps_API.Application.DTO.Todo;
using Devzteps_API.Application.Interfaces.Todo;
using Devzteps_API.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Concurrent;
using System.Diagnostics;

namespace Devzteps_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TodoController : ControllerBase
    {
        private readonly ITodoService _todoService;
        private readonly IMapper _mapper;
        private static int _runningRequests = 0;

        private const int TaskCount = 180;
        private const int TaskDurationMs = 1000;

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

            var resultDto = _mapper.Map<TodoReadItemDTO>(created);

            return CreatedAtAction(nameof(Get), new { id = resultDto.Id }, resultDto);
        }

        // ------------------------------------------------------- Endpoints para Threads e Paralelismo -------------------------

        private void SimulateWork()
        {
            Thread.Sleep(TaskDurationMs);
        }

        [HttpGet("sync-work")]
        public IActionResult SyncWork()
        {
            var stopwatch = Stopwatch.StartNew();

            var threadIds = new HashSet<int>();

            for (int i = 0; i < TaskCount; i++)
            {
                threadIds.Add(Thread.CurrentThread.ManagedThreadId);
                SimulateWork();
            }

            stopwatch.Stop();
            return Ok(new
            {
                Mode = "Synchronous",
                stopwatch.ElapsedMilliseconds,
                ThreadsUsed = threadIds.Count,
                ThreadIds = threadIds.OrderBy(id => id)
            });
        }

        [HttpGet("parallel-work")]
        public async Task<IActionResult> ParallelWork()
        {
            var stopwatch = Stopwatch.StartNew();

            var threadIds = new ConcurrentBag<int>();
            var tasks = new List<Task>();

            for (int i = 0; i < TaskCount; i++)
            {
                tasks.Add(Task.Run(() =>
                {
                    // Captura a thread utilizada
                    threadIds.Add(Thread.CurrentThread.ManagedThreadId);
                    SimulateWork();
                }));
            }

            await Task.WhenAll(tasks);

            stopwatch.Stop();

            return Ok(new
            {
                Mode = "Parallel",
                stopwatch.ElapsedMilliseconds,
                ThreadsUsed = threadIds.Distinct().Count(),
                ThreadIds = threadIds.Distinct().OrderBy(id => id)
            });
        }

        [HttpGet("sync-task")]
        public IActionResult SyncTask()
        {
            Interlocked.Increment(ref _runningRequests);
            Console.WriteLine($"[SYNC TASK] Requisições simultâneas em execução: {_runningRequests} - Thread {Thread.CurrentThread.ManagedThreadId}");

            Thread.Sleep(3000);

            Console.WriteLine($"[SYNC TASK] Finalizando requisição - Thread {Thread.CurrentThread.ManagedThreadId}");
            Interlocked.Decrement(ref _runningRequests);

            return Ok("Tarefa síncrona finalizada.");
        }

        [HttpGet("async-task")]
        public async Task<IActionResult> AsyncTask()
        {
            Interlocked.Increment(ref _runningRequests);
            Console.WriteLine($"[ASYNC TASK] Requisições simultâneas em execução: {_runningRequests} - Thread {Thread.CurrentThread.ManagedThreadId}");

            await Task.Delay(3000);

            Console.WriteLine($"[ASYNC TASK] Finalizando requisição - Thread {Thread.CurrentThread.ManagedThreadId}");
            Interlocked.Decrement(ref _runningRequests);

            return Ok("Tarefa assíncrona finalizada.");
        }
    }
}
