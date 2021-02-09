using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

using HR_Payroll.BusinessObjects;
using HR_Payroll.Data;
using HR_Payroll.Helpers;
using HR_Payroll.Interfaces;
using HR_Payroll.Models;
using HR_Payroll.Repository;

using Newtonsoft.Json;

namespace HR_Payroll.Controllers
{
    public class EmployeesController : Controller
    {
        //private readonly EmployeeFactory EmployeeFactory;
        private readonly IEmployeeRepository employeeRepository;
        private readonly IEmployeesTypeRepository employeesTypeRepository;

        public List<Employee> Employees = new List<Employee>();

        public EmployeesController(IEmployeeRepository _employeeRepository, IEmployeesTypeRepository _employeesTypeRepository)
        {
            //EmployeeFactory = _employeeFactory;
            employeeRepository = _employeeRepository;
            employeesTypeRepository = _employeesTypeRepository;
            //Employees = _employees;
        }

        public IActionResult Index()
        {
            List<Employee> employees = employeeRepository.GetAllData().ToList();
            List<EmployeeViewModel> employeeViewModels = new List<EmployeeViewModel>();

            foreach(var emp in employees)
            {
                EmployeeViewModel viewModel = new EmployeeViewModel()
                {
                    EmployeeIdNo = emp.EmployeeIdNo,
                    EmployeeName = emp.EmployeeName,
                    EmployeeBirthdate = emp.EmployeeBirthdate.ToString("MMMM dd, yyyy"),
                    EmployeeTIN = emp.EmployeeTIN,
                    EmployeeType = Enum.GetName(typeof(Helpers.EmployeeTypeNames), emp.EmployeeType)
                };

                employeeViewModels.Add(viewModel);
            }

            return View(employeeViewModels);
        }

        public IActionResult Create()
        {
            //List<EmployeeType> employeeTypes = EmployeeFactory.GetEmployeesTypeService().GetEmployeeTypes();
            List<EmployeeType> employeeTypes = employeesTypeRepository.GetAllData();
            //int EmployeeCount = EmployeeFactory.GetEmployeesService().GetEmployeesCount();
            int EmployeeCount = (employeeRepository.GetCount() != 0 ? employeeRepository.GetCount() : 1);
            //EmployeeCount = (EmployeeCount != 0 ? EmployeeCount : 1);

            string paddedCount = EmployeeCount.ToString("D4");

            string EmployeeIdNumber = String.Format("EMP-{0}-{1}", DateTime.Now.ToString("yyyy"), paddedCount);

            ViewData["EmployeeType"] = new SelectList(employeeTypes.ToList(), "EmployeeTypeId", "EmployeeTypeName", 1);
            ViewData["EmployeeIdNo"] = EmployeeIdNumber;

            return View();
        }

        [HttpPost]
        public IActionResult Create(Employee employee)
        {
            //EmployeeFactory.GetEmployeesService().CreateEmployee(employee);
            employeeRepository.Create(employee);

            //return Json(new { success = true, responseText = "Employee Has Been Created!" }, new JsonSerializerSettings());
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(string id)
        {
            Employee _employee = employeeRepository.GetData(id);

            EmployeeViewModel employeeViewModel = new EmployeeViewModel
            {
                EmployeeIdNo = _employee.EmployeeIdNo,
                EmployeeName = _employee.EmployeeName,
                EmployeeTIN = _employee.EmployeeTIN,
                EmployeeBirthdate = _employee.EmployeeBirthdate.ToString("MM dd, yyyy"),
                EmployeeType = Enum.GetName(typeof(EmployeeTypeNames), _employee.EmployeeType),
                EmployeeWorkDays = _employee.EmployeeWorkDays.ToString(),
                EmployeeAbsences = _employee.EmployeeAbsences.ToString()
            };

            List<EmployeeType> employeeTypes = employeesTypeRepository.GetAllData();

            decimal TaxRate = 0.0m;
            if((EmployeeTypeNames)_employee.EmployeeType == EmployeeTypeNames.REGULAR)
            {
                TaxRate = 0.12m;
            }

            ViewData["EmployeeType"] = new SelectList(employeeTypes.ToList(), "EmployeeTypeId", "EmployeeTypeName", _employee.EmployeeType);
            ViewData["TaxRate"] = TaxRate;


            return View(employeeViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EmployeeViewModel employeeViewModel)
        {
            employeeRepository.Update(employeeViewModel);

            return RedirectToAction("Index");
        }

        public IActionResult Calculate()
        {
            List<Employee> employees = employeeRepository.GetAllData().ToList();
            employees.Insert(0, new Employee { EmployeeIdNo = "0", EmployeeName = "Select Employee Name" });

            ViewData["Employees"] = new SelectList(employees.ToList(), "EmployeeIdNo", "EmployeeName");

            return View();
        }

        [HttpPost]
        public JsonResult GetCalculatedGrossSalary(string idNo)
        {
            object jsonRes = null;

            Employee employee = employeeRepository.GetData(idNo);

            decimal deductedsalary = 0.0m;

            try
            {
                if (employee.EmployeeType == (int)EmployeeTypeNames.REGULAR)
                {
                    deductedsalary = employee.EmployeeGrossSalary - (employee.EmployeeDailyRate * employee.EmployeeAbsences) - (employee.EmployeeGrossSalary * employee.EmployeeTaxRate);
                }
                else
                {
                    deductedsalary = (employee.EmployeeDailyRate * employee.EmployeeWorkDays);
                }

                List<string> computationresult = new List<string>
                {
                    employee.EmployeeName,
                    employee.EmployeeTIN,
                    Enum.GetName(typeof(EmployeeTypeNames), employee.EmployeeType),
                    employee.EmployeeWorkDays.ToString(),
                    employee.EmployeeAbsences.ToString(),
                    employee.EmployeeDailyRate.ToString(),
                    employee.EmployeeGrossSalary.ToString(),
                    Math.Round(deductedsalary, 2).ToString()
                };


                int totalRecords = computationresult.Count();
                int filteredRecs = computationresult.Count();

                jsonRes = new
                {
                    data = computationresult.ToList()
                };
            }
            catch (InvalidCastException ex)
            {
                throw new InvalidOperationException(ex.Message, ex);
            }
            catch(DivideByZeroException ex)
            {
                throw new DivideByZeroException(ex.Message, ex);
            }

            return new JsonResult(jsonRes);
        }
    }

}