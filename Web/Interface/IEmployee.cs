using MysqlEntity.Core.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Interface
{
    public interface IEmployee
    {
        object EmployeeList(string IsOnLine);
        object SaveEmployee(Employee employee);
        object DelEmployee(int EmpID);
        object EmployeeDetail(int EmpId);
        object EmployeeLeave();
        Task<DataSet> SysDropDown(string DownName);
        object EmployeeLeaveInsert(Employeeleave employeeLeave);
        object DelLeave(int BillID);
        object Employeepayslip(Employeepayslip employeepayslip);
        object payslipList();
        object DelPayslip(int BillID);
        Task<int> InsertDownList(int id);
        Task<int> UpdateDownListName(int id, string Name);
        Task<int> DelDownList(int id);
    }
}
