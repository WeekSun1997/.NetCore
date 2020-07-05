using System;
using System.Collections.Generic;

namespace MysqlEntity.Core.Model
{
    public partial class Vipvisit
    {
        public int BillId { get; set; }
        public string VipPhone { get; set; }
        public string VipName { get; set; }
        public DateTime? VisitDate { get; set; }
        public string Remask { get; set; }
    }
}
