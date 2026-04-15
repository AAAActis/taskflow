using System;

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

        public (string title, string description, string responsible) PedirDatosNuevaTarea()
        {
            WriteLine("\n--- CREAR NUEVA TAREA ---");

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