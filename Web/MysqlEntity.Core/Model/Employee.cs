using System;
using System.Collections.Generic;

namespace MysqlEntity.Core.Model
{
    public partial class Employee
    {
        public int BillId { get; set; }
        public string UserName { get; set; }
        public DateTime? EntryDate { get; set; }
        public DateTime? QuitDate { get; set; }
        public string IdentityId { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string BankCard { get; set; }
        public DateTime? BirthDate { get; set; }
        public string Vxid { get; set; }
        public string Zfbid { get; set; }
        public string BankName { get; set; }
        public string UrgentName { get; set; }
        public string UrgentPhone { get; set; }
        public bool? IsOnLine { get; set; }
        public bool? EmployeeType { get; set; }
        public string Id { get; set; }
    }
}
