using Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;

namespace DBServer
{
    class MSSqlDbServices : IDBServices
    {
        public MSSqlDbServices() { }
        public MSSqlDbServices(string ConnectionString)
        {
            this.ConnectionString = ConnectionString;
        }
        public SqlConnection connection;
        public string ConnectionString { get; set; }
        public object DBContext()
        {
            if (connection == null || connection.State == ConnectionState.Closed)
            {
                connection = new SqlConnection(ConnectionString);
                connection.Open();
            }
            return connection;
        }
        public async Task<DataTable> QueryTable(string sql)
        {
            using (SqlConnection db = DBContext() as SqlConnection)
            {
                    SqlDataAdapter adapter = new SqlDataAdapter(sql, db);
                    DataSet ds = new DataSet();
                    await Task.Run(() => { adapter.Fill(ds); });
                    return ds.Tables[0];
            }
        }
        public async Task<object> ExecuteScalar(string sql)
        {           
                using (SqlConnection conn = DBContext() as SqlConnection)
                {
                    SqlCommand comm = new SqlCommand(sql, conn);
                    return await comm.ExecuteScalarAsync();
                }
        }
        public async Task<int> ExecuteNoQuery(string sql)
        {
                using (SqlConnection conn = DBContext() as SqlConnection)
                {
                    SqlCommand comm = new SqlCommand(sql, conn);
                    return await comm.ExecuteNonQueryAsync();
                }
        }
        public async Task<DataSet> QuerySet(string sql)
        {
            using (SqlConnection db = DBContext() as SqlConnection)
            {
              
                    SqlDataAdapter adapter = new SqlDataAdapter(sql, db);
                    DataSet ds = new DataSet();
                    await Task.Run(() => { adapter.Fill(ds); });
                    return ds;
            }
        }

       
    }
}
