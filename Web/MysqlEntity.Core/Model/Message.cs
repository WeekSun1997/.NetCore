using System;
using System.Collections.Generic;

namespace MysqlEntity.Core.Model
{
    public partial class Message
    {
        public int MsgId { get; set; }
        public string MsgBody { get; set; }
        public int? SendUserId { get; set; }
        public int? ReceiveUserId { get; set; }
        public DateTime? SendTime { get; set; }
        public int? IsSuccess { get; set; }
    }
}
