using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KFramework.DebuggerVisualizers.ListVisualizers.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            SelectColumnInfo item = new SelectColumnInfo();
            item.ColumnName = "Kim Column";
            item.ColumnTitle = "Kim Title";
            item.ID = 1;
            item.CreateDate = DateTime.Now;
            List<SelectColumnInfo> list = Enumerable.Repeat(item, 100).ToList();
       //     ListVisualizer.TestShowVisualizer(list);
            Console.Read();
        }
    }





    public class SelectColumnInfo
    {
        public int ID { get; set; }
        public DateTime CreateDate { get; set; }
        public string ColumnName { get; set; }

        public string ColumnTitle { get; set; }

        public int OrderNum { get; set; }

        public string ColumnTypeString { get; set; }
  
        public bool Selected { get; set; }
    }
}
