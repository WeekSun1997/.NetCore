using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace MongoDB
{
   public class BsonModel
    {
        [BsonId]        //标记主键
        [BsonRepresentation(BsonType.ObjectId)]     //参数类型  ， 无需赋值
        public string Id { get; set; }

      
        public BsonModel()
        {
          
        }
    }
}
