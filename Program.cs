using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Net.WebSockets;
using System.Runtime.CompilerServices;
using System.Xml.Linq;

namespace ConsoleDatabaseViewer
{
    internal class Program
    {
        static void Main(string[] args)
        {
            DynamicDatabase.Welcome();

            using (var context = new DynamicDatabase())
            {
                context.Database.EnsureCreated();
                Console.WriteLine($"Database '{context.dbName}' has been created on the desktop.");

                int numberOfTables;
                if (TryGetIntegerInput("How many tables would you like to add?", out numberOfTables))
                {
                    for (int i = 0; i < numberOfTables; i++)
                    {
                        string tableName = GetStringInput($"Enter the name for table {i + 1}");
                        DynamicTable.tableNames.Add(tableName);
                    }

                    Console.WriteLine($"{context.dbName}'s tables:\n");

                    for (int i = 0; i < numberOfTables; i++)
                    {
                        Console.WriteLine($"{i + 1}: {DynamicTable.tableNames[i]}\n");
                    }

                    foreach (var table in DynamicTable.tableNames)
                    {
                        int numberOfColumns;
                        if (TryGetIntegerInput($"How many columns would you like to add to table: {table}?", out numberOfColumns))
                        {
                            List<string> columnNames = new List<string>();

                            for (int i = 0; i < numberOfColumns; i++)
                            {
                                string columnName = GetStringInput($"Enter the name for column {i + 1}:");
                                columnNames.Add(columnName);
                            }

                            DynamicTable.columnNames[table] = columnNames;
                        }
                    }

                    // Display tables and columns
                    Console.WriteLine("Tables and Columns overview");
                    Console.WriteLine("----------------------------");

                    foreach (var kvp in DynamicTable.columnNames)
                    {
                        string tableName = kvp.Key;
                        List<string> columnList = kvp.Value;

                        Console.WriteLine($"Table Name: {tableName}");

                        foreach (string columnName in columnList)
                        {
                            Console.WriteLine($"Column Name: {columnName}");
                        }

                        Console.WriteLine("----------------------------");
                    }
                }
            }
        }
        static bool TryGetIntegerInput(string prompt, out int result)
        {
            Console.Write($"{prompt}: ");
            if (int.TryParse(Console.ReadLine(), out result))
            {
                return true;
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter a valid integer.");
                return false;
            }
        }
        static string GetStringInput(string prompt)
        {
            string? input;
            do
            {
                Console.Write($"{prompt}: ");
                input = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(input))
                {
                    Console.WriteLine("Error: The input cannot be empty or null");
                }
            } while (string.IsNullOrWhiteSpace(input));

            return input;
        }
    }
}