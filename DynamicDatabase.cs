using Microsoft.EntityFrameworkCore;

namespace ConsoleDatabaseViewer
{
    internal class DynamicDatabase : DbContext
    {
        public string? dbName;
        public string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        public string dbPath;
        public DynamicDatabase() 
        {

            Console.Write("Enter the name for the new database: ");
            dbName = Console.ReadLine();
            while(string.IsNullOrEmpty(dbName))
            {
                Console.WriteLine("Error: The database name cannot be empty or null");
                Console.Write("Enter the name for the new database: ");
                dbName = Console.ReadLine();
            }

            dbPath = Path.Combine(desktopPath, $"{dbName}.db");
            Database.Migrate();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite($"Data Source = {dbPath}");
        }

        public static void Welcome()
        {
            Console.WriteLine("------------------------------");
            Console.WriteLine("|  Console Database Viewer   |");
            Console.WriteLine("------------------------------");
        }
        public static void ShowTablesAndColumns()
        {
            
            Console.WriteLine("Tables and Columns overview");
            Console.WriteLine("----------------------------");
            foreach (var kvp in DynamicTable.columnNames)
            {
                string tableName = kvp.Key;
                List<(string, string)> columnList = kvp.Value;

                Console.WriteLine($"Table Name: {tableName}");
                for (int i = 0; i < columnList.Count; i++)
                {
                    Console.WriteLine($"Column {i + 1} Name: {columnList[i].Item1}, Type: {columnList[i].Item2}");
                }
                Console.WriteLine("----------------------------");
            }
        }
    }
}