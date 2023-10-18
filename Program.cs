using System.Xml.Linq;

namespace ConsoleDatabaseViewer
{
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