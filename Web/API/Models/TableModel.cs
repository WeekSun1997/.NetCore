using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    public class TableModel
    {
        public string tableName { get; set; }
        public string tableType { get; set; }
        public string tableFloat { get; set; }
        public bool tableIsNull { get; set; }
        public string OldtableName { get; set; }
        public bool tableKey { get; set; }
        public bool tableIsAuto { get; set; }
        public string tableLength { get; set; }
        public string OldtableType { get; set; }
    }
}
