using System.Text.Json;
using TaskFlow.Models;

namespace TaskFlow.Services
{
    public class TaskService
    {
        private readonly string _filePath = "data/tasks.json";
        private List<TaskItem> _tasks;

        public TaskService()
        {
            _tasks = CargarTareas();
        }

        public TaskItem CrearTarea(string title, string description, string responsible)
        {
            if (string.IsNullOrWhiteSpace(title))
                throw new ArgumentException("El título es obligatorio.");

            var tarea = new TaskItem
            {
                Id          = _tasks.Count > 0 ? _tasks.Max(t => t.Id) + 1 : 1,
                Title       = title,
                Description = description,
                Responsible = responsible,
                Status = TaskFlow.Models.TaskStatus.Pendiente,
                CreatedAt   = DateTime.Now,
                UpdatedAt   = null
            };

            _tasks.Add(tarea);
            GuardarTareas();
            return tarea;
        }

        private JsonSerializerOptions GetOptions() => new JsonSerializerOptions
{
    WriteIndented = true,
    Converters = { new System.Text.Json.Serialization.JsonStringEnumConverter() }
};

private List<TaskItem> CargarTareas()
{
    if (!File.Exists(_filePath))
        return new List<TaskItem>();

    string json = File.ReadAllText(_filePath);
    return JsonSerializer.Deserialize<List<TaskItem>>(json, GetOptions()) ?? new List<TaskItem>();
}

private void GuardarTareas()
{
    Directory.CreateDirectory("data");
    string json = JsonSerializer.Serialize(_tasks, GetOptions());
    File.WriteAllText(_filePath, json);
}
    }
}