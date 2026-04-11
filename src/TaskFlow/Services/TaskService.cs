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

        private List<TaskItem> CargarTareas()
        {
            if (!File.Exists(_filePath))
                return new List<TaskItem>();

            string json = File.ReadAllText(_filePath);
            return JsonSerializer.Deserialize<List<TaskItem>>(json) ?? new List<TaskItem>();
        }

        private void GuardarTareas()
        {
            Directory.CreateDirectory("data");
            string json = JsonSerializer.Serialize(_tasks, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(_filePath, json);
        }
    }
}