using System;
using System.Collections.Generic;

namespace MysqlEntity.Core.Model
{
    public partial class Employeepayslip
    {
        public int BillId { get; set; }
        public string Id { get; set; }
        public DateTime? PayDate { get; set; }
        public int? UserId { get; set; }
        public string BasePay { get; set; }
        public string Subsidy { get; set; }
        public string Other { get; set; }
        public string WagesPayable { get; set; }
        public string Deduction { get; set; }
        public string Remarks { get; set; }
    }
}
