using System;
using System.Data;
using System.Threading.Tasks;

namespace Interface
{
    public interface IDBServices
    {

        object DBContext();
        Task<DataTable> QueryTable(string sql);        
        Task<object> ExecuteScalar(string sql);
        Task<int> ExecuteNoQuery(string sql);
        Task<DataSet> QuerySet(string sql);
    
    }
}
