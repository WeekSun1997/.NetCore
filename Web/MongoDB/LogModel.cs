using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace MongoDB
{
    public class LogModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public int UserID { get; set; }
        public string UserName { get; set; }
        public string MethodName { get; set; }
        public DateTime SysDate { get; set; }
        public string ExceptionMsg { get; set; }
        public LogModel() { }
        public LogModel(int UserId, string UserName, string MehtodName, DateTime sysdate, string _ExceptionMsg)
        {
            this.UserID = UserId;
            this.UserName = UserName;
            this.MethodName = MethodName;
            this.SysDate = sysdate;
            this.ExceptionMsg = _ExceptionMsg;
        }
    }
}
