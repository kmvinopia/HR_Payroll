using System;
using Xunit;
using Moq;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

using HR_Payroll;
using HR_Payroll.Models;
using System.Threading.Tasks;
using HR_Payroll.Controllers;
using HR_Payroll.Repository;
using HR_Payroll.Interfaces;
using System.Collections.Generic;

namespace xUnitTestProject
{
    public class UnitTest1
    {
        EmployeesController employeesController;

        public UnitTest1()
        {
            
        }

        [Fact]
        public async Task Test1()
        {
            var mockRepo = new Mock<IEmployeeRepository>();
            var mockRepo2 = new Mock<IEmployeesTypeRepository>();
            mockRepo.Setup(repo => repo.GetAllData()).Returns(GetList());

            var testcontroller = new EmployeesController(mockRepo.Object, mockRepo2.Object);

            var result = await testcontroller.Index();

            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<Employee>>(viewResult.ViewData.Model);
            Assert.Null(model);
        }

        private List<Employee> GetList()
        {
            List<Employee> employee = new List<Employee>();
            employee.Add(new Employee()
            {
                EmployeeName = "Test1",
                EmployeeIdNo = "1",
                EmployeeGrossSalary = 10000m,
            });
            employee.Add(new Employee()
            {
                EmployeeName = "Test1",
                EmployeeIdNo = "2",
                EmployeeGrossSalary = 10000m,
            });
            return employee;
        }
    }
}
