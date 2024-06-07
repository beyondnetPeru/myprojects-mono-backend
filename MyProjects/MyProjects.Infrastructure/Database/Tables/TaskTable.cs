namespace MyProjects.Infrastructure.Database.Tables
{
    public class TaskTable
    {
        public string Id { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; } = string.Empty;
        public string? ProjectId { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public int Status { get; set; }
    }
}
