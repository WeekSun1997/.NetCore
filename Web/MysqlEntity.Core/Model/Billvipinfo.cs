﻿using System;
using System.Collections.Generic;

namespace MysqlEntity.Core.Model
{
    public partial class Billvipinfo
    {
        public int BillId { get; set; }
        public string VipName { get; set; }
        public int? Age { get; set; }
        public string Phone { get; set; }
        public int? Sex { get; set; }
    }
}
