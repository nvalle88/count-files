using System;
using System.IO;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Usage: countFiles <directory_path>");

        var directoryPath = Console.ReadLine();

        if (!Directory.Exists(directoryPath))
        {
            Console.WriteLine($"The directory '{directoryPath}' does not exist.");
            return;
        }

        try
        {
            int fileCount = CountFilesRecursively(directoryPath);
            Console.WriteLine($"Number of files in '{directoryPath}' and its subdirectories: {fileCount}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }
    public static int totalFiles { get; set; }

    static int CountFilesRecursively(string directoryPath)
    {
        int count = 0;
        try
        {
            // Count files in the current directory
            count += Directory.GetFiles(directoryPath).Length;
            totalFiles = totalFiles + count;
            WriteInPosition($"Current Directory:{directoryPath}", 3);
            WriteInPosition($"Totals Files:{totalFiles}", 4);
            // Iterate through subdirectories and count files in each one
            foreach (string subdirectory in Directory.GetDirectories(directoryPath))
            {
                count += CountFilesRecursively(subdirectory);
            }
        }
        catch (UnauthorizedAccessException)
        {
            Console.WriteLine($"Access denied to '{directoryPath}'");
        }
        catch (PathTooLongException)
        {
            Console.WriteLine($"Path too long: '{directoryPath}'");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }

        return count;
    }
    static void WriteInPosition(string message, int positionTop)
    {
        Console.SetCursorPosition(0, positionTop);
        Console.Write(message);
    }
}
