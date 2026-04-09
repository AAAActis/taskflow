namespace TaskFlow.Models;

public class TaskItem
{
    public int Id { get; init; }
    public string Title { get; init; }
    public DateTime CreatedAt { get; init; }

    public TaskItem(int id, string title)
    {
        Id = id;
        Title = title;
        CreatedAt = DateTime.UtcNow;
    }
}
