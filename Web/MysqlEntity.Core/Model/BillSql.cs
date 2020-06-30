using System;
using System.Collections.Generic;

namespace MysqlEntity.Core.Model
{
    public partial class Billsql
    {
        public int Id { get; set; }
        public string BillName { get; set; }
        public int? BillId { get; set; }
        public string SqlName { get; set; }
        public string SqlBody { get; set; }
        public string SqlTitle { get; set; }
    }
}
