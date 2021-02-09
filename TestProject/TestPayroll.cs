using Microsoft.VisualStudio.TestTools.UnitTesting;

using HR_Payroll;
using HR_Payroll.Models;
using System.Threading.Tasks;
using HR_Payroll.Controllers;

namespace TestProject
{
    [TestClass]
    public class TestPayroll
    {
        [TestMethod]
        public async Task ValidateRegularEmployeeNetSalary()
        {
            decimal basic_salary = 20000m;
            decimal tax_rate = 0.12m;
            decimal daily_rate = basic_salary / 22;

            var model = new Employee { EmployeeIdNo = "" }; 
        }
    }
}
