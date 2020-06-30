using System;
using System.Collections.Generic;

namespace MysqlEntity.Core.Model
{
    public partial class Vippre
    {
        public int BillId { get; set; }
        public string VipPhone { get; set; }
        public DateTime? PreDate { get; set; }
        public string Project { get; set; }
        public int? Channel { get; set; }
        public int? Statc { get; set; }
        public string Remask { get; set; }
    }
}
