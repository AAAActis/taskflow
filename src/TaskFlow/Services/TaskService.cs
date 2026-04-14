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