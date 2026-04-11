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

        public static void EsperarTecla()
        {
            Console.WriteLine("\nPresione cualquier tecla para continuar...");
            Console.ReadKey();
        }
    }
}
