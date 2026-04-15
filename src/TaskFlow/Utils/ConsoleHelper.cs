using System.Collections.Generic;
using TaskFlow.Models;

namespace TaskFlow.Utils
{
    public static class ConsoleHelper
    {
        public static (string title, string description, string responsible) PedirDatosNuevaTarea()
        {
            Console.WriteLine("\n--- CREAR NUEVA TAREA ---");

            string title;
            do
            {
                Console.Write("Título (obligatorio): ");
                title = Console.ReadLine()?.Trim() ?? string.Empty;
                if (string.IsNullOrWhiteSpace(title))
                    Console.WriteLine("El título no puede estar vacío.");
            } while (string.IsNullOrWhiteSpace(title));

            Console.Write("Descripción (opcional): ");
            string description = Console.ReadLine()?.Trim() ?? string.Empty;

            Console.Write("Responsable: ");
            string responsible = Console.ReadLine()?.Trim() ?? string.Empty;

            return (title, description, responsible);
        }

        public static void MostrarListaTareas(List<TaskItem> tareas)
        {
            Console.WriteLine("\n--- LISTA DE TAREAS ---");
            
            if (tareas.Count == 0)
            {
                Console.WriteLine("No hay tareas registradas en el sistema.");
                return;
            }

            foreach (var t in tareas)
            {
                Console.WriteLine($"[{t.Id}] {t.Title} | Estado: {t.Status} | Responsable: {t.Responsible}");
            }
        }

        public static void EsperarTecla()
        {
            Console.WriteLine("\nPresione cualquier tecla para continuar...");
            Console.ReadKey();
        }
    }
}
