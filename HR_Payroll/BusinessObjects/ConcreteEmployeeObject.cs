using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using HR_Payroll.Interfaces;
using HR_Payroll.Models;

namespace HR_Payroll.BusinessObjects
{
    public class ConcreteEmployeeObject : IEmployees
    {
        public List<Employee> _Employees;

        public ConcreteEmployeeObject()
        {

        }

        public List<Employee> GetEmployees()
        {
            if(_Employees == null)
            {
                _Employees = new List<Employee>();
            }

            return _Employees;
        }

        public Employee GetEmployeeByIdNo(string IdNumber)
        {
            return _Employees.Where(x => x.EmployeeIdNo == IdNumber).FirstOrDefault();
        }

        public void CreateEmployee(Employee employee)
        {
            if (_Employees == null)
            {
                _Employees = new List<Employee>();
            }

            _Employees.Add(employee);
        }

        public int GetEmployeesCount()
        {
            return _Employees.Count;
        }
    }
}
