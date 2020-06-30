using System;
using System.Collections.Generic;

namespace MysqlEntity.Core.Model
{
    public partial class Billtableremask
    {
        public int Id { get; set; }
        public string BillTableName { get; set; }
        public string Remask { get; set; }
        public string FiledName { get; set; }
        public string FiledType { get; set; }
        public string BindList { get; set; }
        public int? TableIndex { get; set; }
        public string Fiedx { get; set; }
        public int? ColLength { get; set; }
    }
}
