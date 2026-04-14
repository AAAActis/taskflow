using TaskFlow.Services;
using TaskFlow.Utils;
using TaskFlow.Models;

var console = new ConsoleHelper();
var service = new TaskService();

bool salir = false;
while (!salir)
{
    System.Console.Clear();
    console.WriteLine("=== TASKFLOW ===");
    console.WriteLine("1. Crear tarea");
    console.WriteLine("2. Listar tareas");
    console.WriteLine("3. Actualizar estado");
    console.WriteLine("4. Cambiar responsable");
    console.WriteLine("5. Eliminar tarea");
    console.WriteLine("6. Salir");
    console.Write("\nOpción: ");

    string opcion = console.ReadLine() ?? string.Empty;

    switch (opcion)
    {
        case "1":
        case "2":
            console.WriteLine("\nFuncionalidad pendiente de implementación.");
            console.WriteLine("\nPresione cualquier tecla para continuar...");
            System.Console.ReadKey();
            break;

        case "3":
            console.WriteLine("\n--- ACTUALIZAR ESTADO DE TAREA ---");
            console.Write("ID de la tarea: ");
            string inputId = console.ReadLine()?.Trim() ?? string.Empty;

            if (!int.TryParse(inputId, out int id))
            {
                console.WriteLine("ID inválido. Debe ser un número entero.");
                console.WriteLine("\nPresione cualquier tecla para continuar...");
                System.Console.ReadKey();
                break;
            }

            console.WriteLine("Nuevo estado:");
            console.WriteLine("  1. Pendiente");
            console.WriteLine("  2. En Progreso");
            console.WriteLine("  3. Completada");
            console.Write("Opción: ");

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
                console.WriteLine("\nPresione cualquier tecla para continuar...");
                System.Console.ReadKey();
                break;
            }

            try
            {
                var tarea = service.ActualizarEstado(id, nuevoEstado.Value);
                string estadoTexto = tarea.Status switch
                {
                    EstadoTarea.Pendiente => "Pendiente",
                    EstadoTarea.EnProgreso => "En Progreso",
                    EstadoTarea.Completada => "Completada",
                    _ => tarea.Status.ToString()
                };
                console.WriteLine($"\n✔ Tarea #{tarea.Id} '{tarea.Title}' actualizada.");
                console.WriteLine($"  Estado:      {estadoTexto}");
                console.WriteLine($"  Actualizada: {tarea.UpdatedAt:dd/MM/yyyy HH:mm:ss}");
            }
            catch (KeyNotFoundException ex)
            {
                console.WriteLine($"\nError: {ex.Message}");
            }

            console.WriteLine("\nPresione cualquier tecla para continuar...");
            System.Console.ReadKey();
            break;

        case "4":
        case "5":
            console.WriteLine("\nFuncionalidad pendiente de implementación.");
            console.WriteLine("\nPresione cualquier tecla para continuar...");
            System.Console.ReadKey();
            break;

        case "6":
            salir = true;
            break;

        default:
            console.WriteLine("\nOpción inválida.");
            console.WriteLine("\nPresione cualquier tecla para continuar...");
            System.Console.ReadKey();
            break;
    }
}