using System;
using System.Collections.Generic;

namespace MysqlEntity.Core.Model
{
    public partial class Friend
    {
        public int Id { get; set; }
        public int? UserId { get; set; }
        public int? FriendId { get; set; }
        public DateTime? RecordTime { get; set; }
    }
}
