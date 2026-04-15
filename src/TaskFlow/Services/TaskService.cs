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

        public List<TaskItem> ListarTareas(TaskFlow.Models.TaskStatus? filtroEstado = null)
        {
            if (filtroEstado.HasValue)
            {
                // Si mandamos un estado, filtramos la lista antes de devolverla
                return _tasks.Where(t => t.Status == filtroEstado.Value).OrderBy(t => t.Id).ToList();
            }
            
            // Si no mandamos nada (es nulo), devuelve todas
            return _tasks.OrderBy(t => t.Id).ToList();
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