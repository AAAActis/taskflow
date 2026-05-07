using System;
using System.Collections.Generic;
using TaskFlow.Models;

namespace TaskFlow.Utils
{
    public class ConsoleHelper
    {
        public void WriteLine(string? message = null)
        {
            if (message is null)
                Console.WriteLine();
            else
                Console.WriteLine(message);
        }

        public void Write(string message)
        {
            Console.Write(message);
        }

        public string? ReadLine()
        {
            return Console.ReadLine();
        }

        public void ClearConsole()
        {
            Console.Clear();
        }

        public void DisplayMenu(string title, string[] options)
        {
            int maxWidth = Math.Max(title.Length, options.Length > 0 ? options.Max(o => o.Length + 3) : 0) + 4;
            string top = "┌" + new string('─', maxWidth) + "┐";
            string bottom = "└" + new string('─', maxWidth) + "┘";
            string titleLine = "│ " + title.PadRight(maxWidth - 2) + " │";

            WriteLine(top);
            WriteLine(titleLine);
            WriteLine("├" + new string('─', maxWidth) + "┤");

            for (int i = 0; i < options.Length; i++)
            {
                string optLine = "│ " + $"{i + 1}. {options[i]}".PadRight(maxWidth - 2) + " │";
                WriteLine(optLine);
            }
            WriteLine(bottom);
        }

        public void DisplayTasksTable(List<TaskItem> tasks)
        {
            if (tasks.Count == 0)
            {
                WriteLine("No hay tareas registradas para mostrar.");
                return;
            }

            WriteLine("┌─────┬─────────────────────────────────────┬─────────────┬─────────────────┐");
            WriteLine("│ ID  │ Título                              │ Estado      │ Creada          │");
            WriteLine("├─────┼─────────────────────────────────────┼─────────────┼─────────────────┤");

            foreach (var t in tasks)
            {
                string status = t.Status switch
                {
                    EstadoTarea.Pendiente => "Pendiente",
                    EstadoTarea.EnProgreso => "En Progreso",
                    EstadoTarea.Completada => "Completada",
                    _ => t.Status.ToString()
                };
                WriteLine($"│ {t.Id.ToString().PadRight(3)} │ {t.Title.PadRight(35)} │ {status.PadRight(11)} │ {t.CreatedAt:dd/MM/yyyy}     │");
            }
            WriteLine("└─────┴─────────────────────────────────────┴─────────────┴─────────────────┘");
        }

        public (string title, string description, string responsible) PedirDatosNuevaTarea()
        {
            WriteLine("\n┌─────────────────────────────┐");
            WriteLine("│       CREAR NUEVA TAREA      │");
            WriteLine("└─────────────────────────────┘");

            string title = ReadNonEmptyString("Título (obligatorio): ");

            Write("Descripción (opcional): ");
            string description = ReadLine()?.Trim() ?? string.Empty;

            Write("Responsable: ");
            string responsible = ReadLine()?.Trim() ?? string.Empty;

            return (title, description, responsible);
        }

        public void EsperarTecla()
        {
            WriteLine("\nPresione cualquier tecla para continuar...");
            Console.ReadKey();
        }

        public string ReadNonEmptyString(string prompt)
        {
            while (true)
            {
                Write(prompt);
                var value = ReadLine();
                if (!string.IsNullOrWhiteSpace(value))
                    return value.Trim();

                WriteLine("Por favor ingrese un valor no vacío.");
            }
        }
    }
}