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
            ScqftrDto item = new ScqftrDto();
            item.ColumnName = "Kim Column";
            item.ColumnTitle = "Kim Title";
            item.ID = 1;
            item.CreateDate = DateTime.Now;
            List<ScqftrDto> list = Enumerable.Repeat(item, 100).ToList();
            ListVisualizer.TestShowVisualizer(list);
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

    //[Serializable]
    public partial class ScqftrDto
    {
        public int ID { get; set; }
        public DateTime CreateDate { get; set; }
        public string ColumnName { get; set; }

        public string ColumnTitle { get; set; }

        public int OrderNum { get; set; }

        public string ColumnTypeString { get; set; }

        public bool Selected { get; set; }
        public int? SCQCOL_ID { get; set; }

        public OperatorTypes OPTYPE { get; set; }

        public int ORDERNUM { get; set; }
        public string DEFVAL { get; set; }
        public string DEFVAL2 { get; set; }
        //EXTEN
        public string COL_NAME { get; set; }
        public int SCQTMP_ID { get; set; }

    }

    public enum OperatorTypes
    {
        Equals = 1,
        Contains = 2,
        StartsWith = 3,
        EndsWith = 4,
        Greater = 5,
        Less = 6,
        GreaterEquals = 7,
        LessEquals = 8,
        Between = 99,
        Range = 100,
    }

}
