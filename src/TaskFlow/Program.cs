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
    console.ClearConsole();
    string[] mainOptions = { "Crear tarea", "Listar tareas", "Actualizar estado", "Cambiar responsable", "Eliminar tarea", "Salir" };
    console.DisplayMenu("TASKFLOW", mainOptions);
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
            console.ClearConsole();
            string[] listOptions = { "Mostrar todas", "Solo Pendientes", "Solo En Progreso", "Solo Completadas" };
            console.DisplayMenu("LISTAR TAREAS", listOptions);
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
            console.DisplayTasksTable(listaTareas);
            console.EsperarTecla();
            break;

        case "3":
            console.ClearConsole();
            console.WriteLine("\n┌─────────────────────────────┐");
            console.WriteLine("│   ACTUALIZAR ESTADO DE TAREA │");
            console.WriteLine("└─────────────────────────────┘");
            Console.Write("ID de la tarea: ");
            string inputId = console.ReadLine()?.Trim() ?? string.Empty;

            if (!int.TryParse(inputId, out int id))
            {
                console.WriteLine("ID inválido. Debe ser un número entero.");
                console.EsperarTecla();
                break;
            }

            string[] stateOptions = { "Pendiente", "En Progreso", "Completada" };
            console.DisplayMenu("NUEVO ESTADO", stateOptions);
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
            console.WriteLine("\n--- ELIMINAR TAREA ---");
            Console.Write("ID de la tarea a eliminar: ");

            string inputIdEliminar = console.ReadLine()?.Trim() ?? string.Empty;

            if (!int.TryParse(inputIdEliminar, out int idEliminar))
            {
                console.WriteLine("ID inválido. Debe ser un número entero.");
                console.EsperarTecla();
                break;
            }

            try
            {
                service.EliminarTarea(idEliminar);
                console.WriteLine($"\n✔ Tarea #{idEliminar} eliminada correctamente del sistema y del JSON.");
            }
            catch (KeyNotFoundException ex)
            {
                console.WriteLine($"\nError: {ex.Message}");
            }

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