using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using HR_Payroll.Models;

namespace HR_Payroll.Interfaces
{
    public interface IEmployeesTypeRepository
    {
        List<EmployeeType> GetAllData();

        EmployeeType GetData(int Id);

        void Create(EmployeeType type);

    }
}
