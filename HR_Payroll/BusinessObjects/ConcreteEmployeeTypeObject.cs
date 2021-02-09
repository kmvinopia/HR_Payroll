using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HR_Payroll.Interfaces;
using HR_Payroll.Models;

namespace HR_Payroll.BusinessObjects
{
    public class ConcreteEmployeeTypeObject
    {
        public List<EmployeeType> _EmployeeTypes;

        public ConcreteEmployeeTypeObject()
        {

        }

        public List<EmployeeType> GetEmployeeTypes()
        {
            if (_EmployeeTypes == null)
            {
                _EmployeeTypes = new List<EmployeeType>
                {
                    new EmployeeType { EmployeeTypeId = 1, EmployeeTypeName = "REGULAR" }, //DEFAULT ITEM
                    new EmployeeType { EmployeeTypeId = 2, EmployeeTypeName = "CONTRACTUAL" } //DEFAULT ITEM
                };
            }

            return _EmployeeTypes;
        }

        public EmployeeType GetEmployeeTypeById(int Id)
        {
            return _EmployeeTypes.Where(x => x.EmployeeTypeId == Id).FirstOrDefault();
        }

        public void CreateEmployeeType(EmployeeType type)
        {
            if (_EmployeeTypes == null)
            {
                _EmployeeTypes = new List<EmployeeType>
                {
                    new EmployeeType { EmployeeTypeId = 1, EmployeeTypeName = "REGULAR" }, //DEFAULT ITEM
                    new EmployeeType { EmployeeTypeId = 2, EmployeeTypeName = "CONTRACTUAL" } //DEFAULT ITEM
                };
            }

            _EmployeeTypes.Add(type);
        }
    }
}
