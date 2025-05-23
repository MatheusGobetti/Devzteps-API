namespace Devzteps_API.Application.DTO
{
    public class TodoCreateItemDTO
    {
        public int Id { get; set; }
        public required string Title { get; set; }
        public bool IsComplete { get; set; }
    }
}
