using Cache;
using MongoDB;
using MongoDB.Bson;
using MysqlEntity.Core.Model;
using StackExchange.Redis;
using System;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {

                MongoDBServer md = new MongoDBServer();
                BsonDocument bd = new BsonDocument();
                var s = new { id = 1 };
                bd.Add(s.ToBsonDocument());
                md.db.GetCollection<MongoDB.Bson.BsonDocument>("t").InsertOne(
                   bd
                    );

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
    }
}
