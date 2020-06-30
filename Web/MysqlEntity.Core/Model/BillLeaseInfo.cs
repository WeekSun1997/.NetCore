using System;
using System.Collections.Generic;

namespace MysqlEntity.Core.Model
{
    public partial class Billleaseinfo
    {
        public int BillId { get; set; }
        public string BillNo { get; set; }
        public string Id { get; set; }
        public string CardNoA { get; set; }
        public string CardNoB { get; set; }
        public string CardPhoneB { get; set; }
        public string CardPhoneA { get; set; }
        public string CardNameA { get; set; }
        public string CardNameB { get; set; }
        public DateTime? PayDate { get; set; }
        public int? PayMode { get; set; }
        public int? LeaseMode { get; set; }
        public string Deposit { get; set; }
        public int? LeaseMonth { get; set; }
    }
}
