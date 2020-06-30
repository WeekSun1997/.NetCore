using System;
namespace EntityFramework.Core.Models
{
    public partial class Message
    {
        public Guid Id { get; set; }
        public string MsgBody { get; set; }
        public Guid SendUserId { get; set; }
        public Guid? ReceiveUserId { get; set; }
        public DateTime SendTime { get; set; }
        public bool? IsSuccess { get; set; }

        public Message() { }
        public Message(Guid Id, string MsgBody, Guid SendUserId, Guid ReceiveUserId, DateTime SendTime, bool IsSuccess)
        {
            this.Id = Id;
            this.MsgBody = MsgBody;
            this.SendUserId = SendUserId;
            this.ReceiveUserId = ReceiveUserId;
            this.SendTime = SendTime;
            this.IsSuccess = IsSuccess;
        }
    }

}
