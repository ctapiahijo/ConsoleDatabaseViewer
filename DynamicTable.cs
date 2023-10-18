using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleDatabaseViewer
{
    internal class DynamicTable
    {
        public static List<string> tableNames = new List<string>();
        public static int numberofcolumns {  get; set; }
        public static Dictionary<string, List<(string, string)>> columnNames = new Dictionary<string, List<(string, string)>>();
    }
}
