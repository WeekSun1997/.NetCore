using Interface;
using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Threading.Tasks;

namespace DBServer
{
    public class MysqlDBServices : IDBServices
    {
        public string ConnectionString { get; set; }

        public MysqlDBServices() { }
        public MySqlConnection dbConnection;
        public MysqlDBServices(string ConnectionString)
        {
            this.ConnectionString = ConnectionString;
        }

        public object DBContext()
        {
            if (dbConnection == null || dbConnection.State != ConnectionState.Open)
            {
                dbConnection = new MySqlConnection(ConnectionString);
                dbConnection.Open();
            }
            return dbConnection;
        }

        public async Task<DataSet> QuerySet(string sql)
        {
            using (MySqlConnection db = DBContext() as MySqlConnection)
            {
                MySqlDataAdapter adapter = new MySqlDataAdapter(sql, db);
                DataSet ds = new DataSet();
                adapter.Fill(ds);
                return ds;
            }

        }
        public async Task<object> ExecuteScalar(string sqltext)
        {
            try
            {
                using (MySqlConnection conn = DBContext() as MySqlConnection)
                {
                    MySqlCommand comm = new MySqlCommand(sqltext, conn);
                    return await comm.ExecuteScalarAsync();
                }
            }
            catch (Exception ex)
            {
                return null;
            }

        }
        public async Task<int> ExecuteNoQuery(string sqltext)
        {
            using (MySqlConnection conn = DBContext() as MySqlConnection)
            {
                MySqlCommand comm = new MySqlCommand(sqltext, conn);
                return await comm.ExecuteNonQueryAsync();
            }
        }
        public async Task<DataTable> QueryTable(string sql)
        {
            using (MySqlConnection db = DBContext() as MySqlConnection)
            {
                MySqlDataAdapter adapter = new MySqlDataAdapter(sql, db);
                DataSet ds = new DataSet();
                adapter.Fill(ds);
                return ds.Tables[0];
            }
        }


    }
}
