﻿namespace Devzteps_API.Domain.Entities
{
    public class TodoItem
    {
        public int Id { get; set; }
        public required string Title { get; set; }
        public bool IsComplete { get; set; }
    }
}
