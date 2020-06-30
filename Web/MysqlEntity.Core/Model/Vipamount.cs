using System;
using System.Collections.Generic;

namespace MysqlEntity.Core.Model
{
    public partial class Vipamount
    {
        public int BillId { get; set; }
        public int? VipId { get; set; }
        public int? Channel { get; set; }
        public int? Statc { get; set; }
        public string Project { get; set; }
        public decimal? Amount { get; set; }
        public int? TransactionType { get; set; }
        public string Remask { get; set; }
    }
}
