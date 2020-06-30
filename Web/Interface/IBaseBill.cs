using Model;
using MysqlEntity.Core.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Interface
{
    public interface IBaseBill
    {
       Task<DataTable> GetTable();
        object AddTable(List<TableModel> data, string TableName, string type);
        object LoadTableCloums(string tableName);
        Task<DataTable> LoadTable(string tableName);
        object ExceSql(string Sql, string SqlType);
        object BaseTable(string BillNo, string SearchStr);
        object BaseTableDel(string BillNo, int BillID);
        object DelTable(string tableName);
        object BaseSave(string BillID, string Mb);
        object BaseInfo(int id = 0);
        object BaseFrom(string BillNo, string BillID);
        object BaseRemask(string TableName);
        object BaseRemaskSave(string JsonData, string TableName);
        object BaseSearch(string BillNo);
        object BaseBillSave(Billmodularinfo billmodularinfo);
    }
}
