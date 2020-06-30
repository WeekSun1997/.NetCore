using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MongoDB
{
    public class MongoDBServer
    {
        public static string MongoDbConnectionString { get; set; }

        public static string MongoDbDataBase { get; set; }

        public MongoClient mongo;
        public IMongoDatabase db;
        public MongoDBServer()
        {
            if (mongo == null)
            {
                mongo = new MongoClient(MongoDbConnectionString);
            }
            if (db == null)
            {
                db = mongo.GetDatabase(MongoDbDataBase);
            }
        }

        public void InsertOne<T>(T t, string Name)
        {
            db.GetCollection<T>(Name).InsertOne(t);
        }
        public async Task InsertOneAsync<T>(T t, string Name)
        {
             await db.GetCollection<T>(Name).InsertOneAsync(t);
        }
      
    }
}
