using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace MongoDB
{
   public class ExceptionLog
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public int UserID { get; set; }
        public string UserName { get; set; }
        public DateTime SysDate { get; set; }
        public string ExceptionMsg { get; set; }
        public string ExceptionDetail { get; set; }
        public ExceptionLog() { }
        public ExceptionLog(int UserId, string UserName, DateTime sysdate, string _ExceptionMsg, string ExceptionDetail)
        {
            this.UserID = UserId;
            this.UserName = UserName;
            this.ExceptionDetail = ExceptionDetail;
            this.SysDate = sysdate;
            this.ExceptionMsg = _ExceptionMsg;
        }
    }
}
