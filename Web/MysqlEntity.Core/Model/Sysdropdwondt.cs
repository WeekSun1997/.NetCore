using System;
using System.Collections.Generic;

namespace MysqlEntity.Core.Model
{
    public partial class Sysdropdwondt
    {
        public int Id { get; set; }
        public int? BillId { get; set; }
        public string Value { get; set; }
    }
}
