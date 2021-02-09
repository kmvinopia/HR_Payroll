using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HR_Payroll.Models
{
    public class Employee
    {
        [Display(Name = "Employee ID")]
        public string EmployeeIdNo { get; set; }

        [Display(Name = "Employee Name")]
        public string EmployeeName { get; set; }

        [Display(Name = "Birth Date")]
        public DateTime EmployeeBirthdate { get; set; }

        [Display(Name = "Tax Identification Number")]
        public string EmployeeTIN { get; set; }

        [Display(Name = "Employee Type")]
        public int EmployeeType { get; set; }

        [Display(Name = "Work Days")]
        public decimal EmployeeWorkDays { get; set; }

        [Display(Name = "Absences")]
        public decimal EmployeeAbsences { get; set; }

        [Display(Name = "Gross Salary")]
        public decimal EmployeeGrossSalary { get; set; }

        [Display(Name = "Deducted Salary")]
        public decimal EmployeeDeductedSalary { get; set; }

        [Display(Name = "Daily Rate")]
        public decimal EmployeeDailyRate { get; set; }

        [Display(Name = "Tax Rate")]
        public decimal EmployeeTaxRate { get; set; }
    }

    public class EmployeeViewModel
    {
        [Display(Name = "Employee ID")]
        public string EmployeeIdNo { get; set; }

        [Display(Name = "Employee Name")]
        public string EmployeeName { get; set; }

        [Display(Name = "Birth Date")]
        public string EmployeeBirthdate { get; set; }

        [Display(Name = "Tax Identification Number")]
        public string EmployeeTIN { get; set; }

        [Display(Name = "Employee Type")]
        public string EmployeeType { get; set; }

        [Display(Name = "Work Days")]
        public string EmployeeWorkDays { get; set; }

        [Display(Name = "Absences")]
        public string EmployeeAbsences { get; set; }

        [Display(Name = "Gross Salary")]
        public string EmployeeGrossSalary { get; set; }

        [Display(Name = "Deducted Salary")]
        public string EmployeeDeductedSalary { get; set; }

        [Display(Name = "Daily Rate")]
        public string EmployeeDailyRate { get; set; }

        [Display(Name = "Tax Rate")]
        public string EmployeeTaxRate { get; set; }
    }

}
