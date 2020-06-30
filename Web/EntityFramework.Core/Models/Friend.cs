using System;
using System.Collections.Generic;

namespace EntityFramework.Core.Models
{
    public partial class Friend
    {
        public Guid Id { get; set; }
        public Guid? UserId { get; set; }
        public Guid? FriendId { get; set; }
        public DateTime? RecordTime { get; set; }
    }
}
