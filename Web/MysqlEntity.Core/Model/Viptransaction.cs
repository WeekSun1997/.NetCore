using System;
using System.Collections.Generic;

namespace MysqlEntity.Core.Model
{
    public partial class Viptransaction
    {
        public int BillId { get; set; }
        public string VipName { get; set; }
        public DateTime? CreateTime { get; set; }
        public string Phone { get; set; }
        public int? Channel { get; set; }
        public int? Stuts { get; set; }
        public string Project { get; set; }
        public decimal? Amount { get; set; }
        public int? TransactionType { get; set; }
        public string Remask { get; set; }
    }
}
