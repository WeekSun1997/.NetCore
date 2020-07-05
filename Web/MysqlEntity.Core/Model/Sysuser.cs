using System;
using System.Collections.Generic;

namespace MysqlEntity.Core.Model
{
    public partial class Sysuser
    {
        public int BillId { get; set; }
        public string UserName { get; set; }
        public string PassWrod { get; set; }
        public string UserCode { get; set; }
    }
}
