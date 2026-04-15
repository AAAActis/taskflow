namespace TaskFlow.Models
{
    public enum TaskStatus
    {
        Pendiente,
        EnProgreso,
        Completada
    }

    public class TaskItem
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Responsible { get; set; } = string.Empty;
        public TaskStatus Status { get; set; } = TaskStatus.Pendiente;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? UpdatedAt { get; set; }
    }
}