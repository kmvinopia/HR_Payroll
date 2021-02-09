using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using HR_Payroll.Interfaces;
using HR_Payroll.Models;



namespace HR_Payroll.Repository
{
    public class EmployeeTypeRepository : IEmployeesTypeRepository
    {
        private readonly List<EmployeeType> EmployeeTypeList;

        public EmployeeTypeRepository()
        {
            EmployeeTypeList = new List<EmployeeType>
            {
                new EmployeeType { EmployeeTypeId = 1, EmployeeTypeName = "REGULAR" }, //DEFAULT ITEM
                new EmployeeType { EmployeeTypeId = 2, EmployeeTypeName = "CONTRACTUAL" } //DEFAULT ITEM
            };
        }

        public void Create(EmployeeType type)
        {
            EmployeeTypeList.Add(type);
        }

        public List<EmployeeType> GetAllData()
        {
            return EmployeeTypeList.ToList();
        }

        public EmployeeType GetData(int Id)
        {
            return EmployeeTypeList.Where(x => x.EmployeeTypeId == Id).FirstOrDefault();
        }
    }
}
