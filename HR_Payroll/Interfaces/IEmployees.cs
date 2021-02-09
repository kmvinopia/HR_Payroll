using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using HR_Payroll.BusinessObjects;
using HR_Payroll.Models;

namespace HR_Payroll.Interfaces
{
    public interface IEmployees
    {
        List<Employee> GetEmployees();

        Employee GetEmployeeByIdNo(string IdNumber);

        void CreateEmployee(Employee employee);

        int GetEmployeesCount();
    }
}
