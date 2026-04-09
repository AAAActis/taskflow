using TaskFlow.Services;
using TaskFlow.Utils;

var service = new TaskService();

bool salir = false;
while (!salir)
{
    Console.Clear();
    Console.WriteLine("=== TASKFLOW ===");
    Console.WriteLine("1. Crear tarea");
    Console.WriteLine("2. Listar tareas");
    Console.WriteLine("3. Actualizar estado");
    Console.WriteLine("4. Cambiar responsable");
    Console.WriteLine("5. Eliminar tarea");
    Console.WriteLine("6. Salir");
    Console.Write("\nOpción: ");

    string opcion = Console.ReadLine() ?? string.Empty;

    switch (opcion)
    {
        case "1":
            var (title, desc, resp) = ConsoleHelper.PedirDatosNuevaTarea();
            var tarea = service.CrearTarea(title, desc, resp);
            Console.WriteLine($"\nTarea #{tarea.Id} '{tarea.Title}' creada correctamente.");
            ConsoleHelper.EsperarTecla();
            break;

        case "2":
        case "3":
        case "4":
        case "5":
            Console.WriteLine("\nFuncionalidad pendiente de implementación.");
            ConsoleHelper.EsperarTecla();
            break;

        case "6":
            salir = true;
            break;

        default:
            Console.WriteLine("\nOpción inválida.");
            ConsoleHelper.EsperarTecla();
            break;
    }
}