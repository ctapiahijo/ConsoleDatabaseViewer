using Microsoft.EntityFrameworkCore;

namespace ConsoleDatabaseViewer
{
    internal class DynamicDatabase : DbContext
    {
        public string? dbName;
        public string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        public string dbPath;
        public string dynamicQuery = string.Empty;
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
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite($"Data Source = {dbPath}");
        }


    }
}