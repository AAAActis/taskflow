using System;
using System.Collections.Generic;
using TaskFlow.Services;
using TaskFlow.Utils;
using TaskFlow.Models;

var console = new ConsoleHelper();
var service = new TaskService();

bool salir = false;
while (!salir)
{
    Console.Clear();
    console.WriteLine("=== TASKFLOW ===");
    console.WriteLine("1. Crear tarea");
    console.WriteLine("2. Listar tareas");
    console.WriteLine("3. Actualizar estado");
    console.WriteLine("4. Cambiar responsable");
    console.WriteLine("5. Eliminar tarea");
    console.WriteLine("6. Salir");
    Console.Write("\nOpción: ");

    string opcion = console.ReadLine() ?? string.Empty;

    switch (opcion)
    {
        case "1":
            var (title, desc, resp) = console.PedirDatosNuevaTarea();
            var nuevaTarea = service.CrearTarea(title, desc, resp);
            console.WriteLine($"\n✔ Tarea #{nuevaTarea.Id} '{nuevaTarea.Title}' creada correctamente.");
            console.EsperarTecla();
            break;

        case "2":
            Console.Clear();
            console.WriteLine("--- LISTAR TAREAS ---");
            console.WriteLine("1. Mostrar todas");
            console.WriteLine("2. Solo Pendientes");
            console.WriteLine("3. Solo En Progreso");
            console.WriteLine("4. Solo Completadas");
            Console.Write("\nSeleccione una opción de filtrado (1-4): ");

            string opcionFiltro = console.ReadLine() ?? "1";
            EstadoTarea? estadoFiltro = null; 

            switch (opcionFiltro)
            {
                case "2": estadoFiltro = EstadoTarea.Pendiente; break;
                case "3": estadoFiltro = EstadoTarea.EnProgreso; break;
                case "4": estadoFiltro = EstadoTarea.Completada; break;
            }

            var listaTareas = service.ListarTareas(estadoFiltro);

            console.WriteLine("\n--- RESULTADO ---");
            if (listaTareas.Count == 0)
            {
                console.WriteLine("No hay tareas registradas para mostrar.");
            }
            else
            {
                foreach (var t in listaTareas)
                {
                    console.WriteLine($"ID: {t.Id} | Título: {t.Title} | Resp: {t.Responsible} | Estado: {t.Status} | Creada: {t.CreatedAt:dd/MM/yyyy}");
                }
            }
            console.EsperarTecla();
            break;

        case "3":
            console.WriteLine("\n--- ACTUALIZAR ESTADO DE TAREA ---");
            Console.Write("ID de la tarea: ");
            string inputId = console.ReadLine()?.Trim() ?? string.Empty;

            if (!int.TryParse(inputId, out int id))
            {
                console.WriteLine("ID inválido. Debe ser un número entero.");
                console.EsperarTecla();
                break;
            }

            console.WriteLine("Nuevo estado:");
            console.WriteLine("  1. Pendiente");
            console.WriteLine("  2. En Progreso");
            console.WriteLine("  3. Completada");
            Console.Write("Opción: ");

            string opcionEstado = console.ReadLine()?.Trim() ?? string.Empty;

            EstadoTarea? nuevoEstado = opcionEstado switch
            {
                "1" => EstadoTarea.Pendiente,
                "2" => EstadoTarea.EnProgreso,
                "3" => EstadoTarea.Completada,
                _ => null
            };

            if (nuevoEstado is null)
            {
                console.WriteLine("Opción de estado inválida.");
                console.EsperarTecla();
                break;
            }

            try
            {
                var tareaActualizada = service.ActualizarEstado(id, nuevoEstado.Value);
                string estadoTexto = tareaActualizada.Status switch
                {
                    EstadoTarea.Pendiente => "Pendiente",
                    EstadoTarea.EnProgreso => "En Progreso",
                    EstadoTarea.Completada => "Completada",
                    _ => tareaActualizada.Status.ToString()
                };
                console.WriteLine($"\n✔ Tarea #{tareaActualizada.Id} '{tareaActualizada.Title}' actualizada.");
                console.WriteLine($"  Estado:      {estadoTexto}");
                console.WriteLine($"  Actualizada: {tareaActualizada.UpdatedAt:dd/MM/yyyy HH:mm:ss}");
            }
            catch (KeyNotFoundException ex)
            {
                console.WriteLine($"\nError: {ex.Message}");
            }

            console.EsperarTecla();
            break;

        case "4":
        case "5":
            console.WriteLine("\nFuncionalidad pendiente de implementación.");
            console.EsperarTecla();
            break;

        case "6":
            salir = true;
            break;

        default:
            console.WriteLine("\nOpción inválida.");
            console.EsperarTecla();
            break;
    }
}