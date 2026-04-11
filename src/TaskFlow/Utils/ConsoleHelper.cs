namespace TaskFlow.Utils;

public class ConsoleHelper
{
    public void WriteLine(string? message = null)
    {
        if (message is null)
            System.Console.WriteLine();
        else
            System.Console.WriteLine(message);
    }

    public void Write(string message)
    {
        System.Console.Write(message);
    }

    public string? ReadLine()
    {
        return System.Console.ReadLine();
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