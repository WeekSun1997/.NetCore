using System;
using System.Collections.Generic;

namespace MysqlEntity.Core.Model
{
    public partial class Billmodularinfo
    {
        public int Id { get; set; }
        public int? BillId { get; set; }
        public string Modulardtnametext { get; set; }
        public string ModularInfoId { get; set; }
        public string ModularInfoname { get; set; }
        public string ModularInfoulr { get; set; }
        public string BillTable { get; set; }
        public bool? Islist { get; set; }
        public string IsShow { get; set; }
    }
}
