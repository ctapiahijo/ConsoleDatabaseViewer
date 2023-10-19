

using Microsoft.EntityFrameworkCore;
using System.Text;

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
                Console.WriteLine($"Database '{context.dbName}' has been created.");

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
                            List<(string, string)> columnNames = new List<(string, string)>();

                            for (int i = 0; i < numberOfColumns; i++)
                            {
                                string columnName = GetStringInput($"Enter the name for column {i + 1}:");
                                string columnType;
                                Console.WriteLine($"Select type for column {i + 1} for table {table} (\nPress 1 for NULL\nPress 2 for INTEGER\nPress 3 for REAL\nPress 4 for TEXT\nPress 5 for BLOB\n");
                                string UserInput = Console.ReadLine();
                                switch (UserInput)
                                {
                                    case "1":
                                        columnType = "NULL";
                                        break;
                                    case "2":
                                        columnType = "INTEGER";
                                        break;
                                    case "3":
                                        columnType = "REAL";
                                        break;
                                    case "4":
                                        columnType = "TEXT";
                                        break;
                                    case "5":
                                        columnType = "BLOB";
                                        break;
                                    default:
                                        columnType = "INTEGER";
                                        break;
                                }
                                

                                columnNames.Add((columnName, columnType));
                            }

                            DynamicTable.columnNames[table] = columnNames;

                            foreach (var table_x in DynamicTable.columnNames)
                            {
                                string tableName = table_x.Key;
                                List<(string, string)> columnList = table_x.Value;

                                var createTableSql = new StringBuilder();
                                createTableSql.Append($"CREATE TABLE {tableName} (");

                                foreach (var column in columnList)
                                {
                                    createTableSql.Append($"{column.Item1} {column.Item2}, ");
                                }

                                createTableSql.Remove(createTableSql.Length - 2, 2); // Remove the trailing comma and space
                                createTableSql.Append(");");

                               
                                context.Database.ExecuteSqlRaw(createTableSql.ToString());
                            }

                        }
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
