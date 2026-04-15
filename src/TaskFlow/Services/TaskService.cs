using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using TaskFlow.Models;

namespace TaskFlow.Services
{
    public class TaskService
    {
        private readonly string _directoryPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "data");
        private readonly string _filePath;

        private List<TaskItem> _tasks;

        public TaskService()
        {
            _filePath = Path.Combine(_directoryPath, "tasks.json");
            _tasks = CargarTareas();
        }

        public TaskItem CrearTarea(string title, string description, string responsible)
        {
            if (string.IsNullOrWhiteSpace(title))
                throw new ArgumentException("El título es obligatorio.");

            var tarea = new TaskItem
            {
                Id = _tasks.Count > 0 ? _tasks.Max(t => t.Id) + 1 : 1,
                Title = title,
                Description = description,
                Responsible = responsible,
                Status = EstadoTarea.Pendiente, 
                CreatedAt = DateTime.Now,
                UpdatedAt = null
            };

            _tasks.Add(tarea);
            GuardarTareas();
            return tarea;
        }

        public List<TaskItem> ListarTareas(EstadoTarea? filtroEstado = null)
        {
            if (filtroEstado.HasValue)
            {
                return _tasks.Where(t => t.Status == filtroEstado.Value).OrderBy(t => t.Id).ToList();
            }

            return _tasks.OrderBy(t => t.Id).ToList();
        }

        public TaskItem ActualizarEstado(int id, EstadoTarea nuevoEstado)
        {
            var tarea = _tasks.FirstOrDefault(t => t.Id == id)
                ?? throw new KeyNotFoundException($"No se encontró la tarea con ID {id}.");

            tarea.Status = nuevoEstado;
            tarea.UpdatedAt = DateTime.Now;

            GuardarTareas();
            return tarea;
        }

        private List<TaskItem> CargarTareas()
        {
            if (!File.Exists(_filePath))
                return new List<TaskItem>();

            try
            {
                string json = File.ReadAllText(_filePath);
                return JsonSerializer.Deserialize<List<TaskItem>>(json) ?? new List<TaskItem>();
            }
            catch
            {
                return new List<TaskItem>();
            }
        }

        private void GuardarTareas()
        {
            if (!Directory.Exists(_directoryPath))
            {
                Directory.CreateDirectory(_directoryPath);
            }

            string json = JsonSerializer.Serialize(_tasks, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(_filePath, json);
        }
    }
}