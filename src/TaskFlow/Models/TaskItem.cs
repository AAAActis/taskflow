namespace TaskFlow.Models;

public class TaskItem
{
    public int Id { get; init; }
    public string Title { get; init; }
    public string Description { get; set; } = string.Empty;
    public string Responsible { get; set; } = string.Empty;
    public EstadoTarea Status { get; set; } = EstadoTarea.Pendiente;
    public DateTime CreatedAt { get; init; }
    public DateTime? UpdatedAt { get; set; }

    public TaskItem(int id, string title)
    {
        Id = id;
        Title = title;
        CreatedAt = DateTime.UtcNow;
    }

    public enum EstadoTarea
    {
        Pendiente,
        EnProgreso,
        Completada
    }
}