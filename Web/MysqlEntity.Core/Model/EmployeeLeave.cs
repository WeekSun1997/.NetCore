using System;
using System.Collections.Generic;

namespace MysqlEntity.Core.Model
{
    public partial class Employeeleave
    {
        public int BillId { get; set; }
        public int UserId { get; set; }
        public DateTime? LeaveDate { get; set; }
        public bool? LeaveType { get; set; }
        public int? LeaveReasons { get; set; }
        public DateTime? Overtime { get; set; }
        public string Id { get; set; }
        public string UserName { get; set; }
    }
}
