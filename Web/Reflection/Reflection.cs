using System;
using System.Linq.Expressions;
using System.Reflection;
using Interface;
using MysqlEntity.Core.Model;
namespace Reflection
{
    public class Reflection : IAssembly
    {
        public Type GetTableType(string TableName)
        {

            if (TableName == "Billleaseinfo")
            {
                var model = new Billleaseinfo();
                return model.GetType();
            }
            return null;
        }
      
    }
}
