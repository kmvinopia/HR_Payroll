using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using HR_Payroll.BusinessObjects;
using HR_Payroll.Interfaces;
using HR_Payroll.Models;

namespace HR_Payroll.Data
{
    public class EmployeeFactory
    {
        public IEmployees GetEmployeesService()
        {
            return new ConcreteEmployeeObject();
        }

        //public IEmployeesType GetEmployeesTypeService()
        //{
        //    return new ConcreteEmployeeTypeObject();
        //}
    }
}
