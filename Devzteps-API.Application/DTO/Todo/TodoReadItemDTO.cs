namespace Devzteps_API.Application.DTO.Todo
{
    public class TodoReadItemDTO
    {
        public int Id { get; set; }
        public required string Title { get; set; }
        public bool IsComplete { get; set; }
    }
}
