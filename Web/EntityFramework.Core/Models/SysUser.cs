using System;
using System.Collections.Generic;

namespace EntityFramework.Core.Models
{
    public partial class SysUser
    {
        public Guid UserId { get; set; }
        public string UserName { get; set; }
        public string Passwrod { get; set; }
    }
}
