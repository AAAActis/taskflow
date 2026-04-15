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
            Console.Clear();
            Console.WriteLine("--- LISTAR TAREAS ---");
            Console.WriteLine("1. Mostrar todas");
            Console.WriteLine("2. Solo Pendientes");
            Console.WriteLine("3. Solo En Progreso");
            Console.WriteLine("4. Solo Completadas");
            Console.Write("\nSeleccione una opción de filtrado (1-4): ");
            
            string opcionFiltro = Console.ReadLine() ?? "1";
            TaskFlow.Models.TaskStatus? estadoFiltro = null;

            switch (opcionFiltro)
            {
                case "2": estadoFiltro = TaskFlow.Models.TaskStatus.Pendiente; break;
                case "3": estadoFiltro = TaskFlow.Models.TaskStatus.EnProgreso; break;
                case "4": estadoFiltro = TaskFlow.Models.TaskStatus.Completada; break;
                // El caso 1 (o cualquier input raro) deja el filtro nulo para traer todas
            }

            var listaTareas = service.ListarTareas(estadoFiltro);

            Console.WriteLine("\n--- RESULTADO ---");
            if (listaTareas.Count == 0)
            {
                Console.WriteLine("No hay tareas registradas con ese estado.");
            }
            else
            {
                foreach (var t in listaTareas)
                {
                    Console.WriteLine($"ID: {t.Id} | Título: {t.Title} | Resp: {t.Responsible} | Estado: {t.Status} | Creada: {t.CreatedAt:dd/MM/yyyy}");
                }
            }
            ConsoleHelper.EsperarTecla();
            break;

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