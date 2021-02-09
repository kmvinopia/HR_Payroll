using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using HR_Payroll.Models;
using Microsoft.AspNetCore.Mvc;

namespace HR_Payroll.Interfaces
{
    public interface IEmployeeRepository
    {
        List<Employee> GetAllData();

        Employee GetData(string EmployeeNo);

        void Create(Employee _item);

        void Update(EmployeeViewModel tempEmployee);

        void Delete(string IdNo);

        int GetCount();

    }
}
