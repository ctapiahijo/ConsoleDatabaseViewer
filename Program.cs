using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Net.WebSockets;
using System.Runtime.CompilerServices;
using System.Xml.Linq;

namespace ConsoleDatabaseViewer
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            await Console.Out.WriteLineAsync("------------------------------");
            await Console.Out.WriteLineAsync("|  Console Database Viewer   |");
            await Console.Out.WriteLineAsync("------------------------------");

            using (var context = new DynamicDatabase())
            {
                context.Database.EnsureCreated();
                Console.WriteLine($"Database '{context.dbName}' has been created on the desktop.");
                await Console.Out.WriteLineAsync($"How many tables would you like to add to {context.dbName}?");
                if(int.TryParse(Console.ReadLine(), out int numberoftables))
                {
                    string tablename;
                    for (int i = 0; i < numberoftables; i++)
                    {
                            await Console.Out.WriteAsync($"Enter the name for table {i + 1}: ");
                            tablename = Console.ReadLine();
                        while(string.IsNullOrEmpty(tablename))
                        {
                            await Console.Out.WriteAsync("Error: The table name cannot be empty or null\n");
                            await Console.Out.WriteAsync($"Enter the name for table {i + 1}: ");
                            tablename = Console.ReadLine();
                        }
                        DynamicTable.tableNames.Add(tablename);
                    }

                    await Console.Out.WriteLineAsync($"{context.dbName}'s tables:\n");
                    for(int i = 0; i < numberoftables; ++i)
                    {
                        await Console.Out.WriteLineAsync($"{i + 1}:{ DynamicTable.tableNames[i]}");
                        await Console.Out.WriteLineAsync("--------------------------------------");
                    }
                    int numberofcolumns = 0;
                    List<string> columnNames = new List<string>();
                    foreach (var table in DynamicTable.tableNames)
                    {
                        await Console.Out.WriteLineAsync($"How many columns would you like to add to table: {table}: ");
                        numberofcolumns = int.Parse(Console.ReadLine());
                        await Console.Out.WriteLineAsync($"Please enter the name of the column for table: {table}: (Please be aware that the first column will be the primary key)");
                        for (int i = 0; i < numberofcolumns; ++i)
                        {
                            await Console.Out.WriteAsync($"Column {i + 1}:");
                            string value = Console.ReadLine();
                            columnNames.Add(value);
                        }
                        DynamicTable.columnNames[table] = columnNames;
                    }


                    await Console.Out.WriteLineAsync($"Tables and Columns overview");
                    await Console.Out.WriteLineAsync("----------------------------");
                    foreach (var kvp in DynamicTable.columnNames)
                    {
                        string tableName = kvp.Key;
                        List<string> columnList = kvp.Value;

                        Console.WriteLine($"Table Name: {tableName}");

                        foreach (string columnName in columnList)
                        {
                            Console.WriteLine($"Column Name: {columnName}");
                        }

                        Console.WriteLine("--------------------------");
                    }
                }
            }
        }
    }
}