using TaskFlow.Models;

namespace TaskFlow.Services;

public class TaskService
{
    private readonly List<TaskItem> _tasks = new();
    private int _nextId = 1;

    public TaskItem AddTask(string title)
    {
        var newTask = new TaskItem(_nextId++, title);
        _tasks.Add(newTask);
        return newTask;
    }

    public IReadOnlyList<TaskItem> GetAll() => _tasks.AsReadOnly();
}
