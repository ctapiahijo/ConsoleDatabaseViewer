using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;

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
            if (string.IsNullOrWhiteSpace(dbName))
            {
                Console.WriteLine("Database name cannot be empty. Exiting.");
                Environment.Exit(1);
            }

            dbPath = Path.Combine(desktopPath, $"{dbName}.db");
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite($"Data Source = {dbPath}");
        }
    }
    internal class Program
    {
        static async Task Main(string[] args)
        {
            using (var context = new DynamicDatabase())
            {
                context.Database.EnsureCreated();
                Console.WriteLine($"Database '{context.dbName}' created on the desktop.");
            }
        }
    }
}