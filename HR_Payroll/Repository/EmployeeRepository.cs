using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Threading.Tasks;

using HR_Payroll.Models;
using HR_Payroll.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using HR_Payroll.Helpers;

namespace HR_Payroll.Repository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        //private readonly List<Employee> EmployeeList;
        private readonly string AppDir;

        public EmployeeRepository()
        {
            //EmployeeList = new List<Employee>();
            AppDir = AppDomain.CurrentDomain.BaseDirectory;
        }

        public List<Employee> GetAllData()
        {
            List<Employee> items = new List<Employee>();

            String filename = String.Concat(AppDir, "\\", "employees.txt");
            using (StreamReader sr = File.OpenText(filename))
            {
                string jsonData = sr.ReadToEnd();
                //var item1 = JsonConvert.DeserializeObject<List<Employee>>(jsonData);
                items = JsonConvert.DeserializeObject<List<Employee>>(jsonData);
            }

            return items.ToList();
        }

        public int GetCount()
        {
            int listCount = 0;

            String filename = String.Concat(AppDir, "\\", "employees.txt");
            using (StreamReader sr = File.OpenText(filename))
            {
                string jsonData = sr.ReadToEnd();
                //var item1 = JsonConvert.DeserializeObject<List<Employee>>(jsonData);
                var items = JsonConvert.DeserializeObject<List<Employee>>(jsonData);
                listCount = items.Count;
            }

            return listCount;
        }

        public void Create(Employee _item)
        {
            String filename = String.Concat(AppDir, "\\", "employees.txt");
            List<Employee> emp = new List<Employee>();

            if (!File.Exists(filename))
            {
                StreamWriter sw = new StreamWriter(filename, true);
            }

            string json = File.ReadAllText(filename);
            var tempEmployeeJson = JsonConvert.DeserializeObject<List<Employee>>(json);
            if(tempEmployeeJson != null)
            {
                foreach(var employee in tempEmployeeJson)
                {
                    emp.Add(employee);
                }
            }
            else
            {
                emp.Add(_item);
            }

            string newJson = JsonConvert.SerializeObject(emp);
            File.WriteAllText(filename, newJson);
        }

        public Employee GetData(string id)
        {
            String filename = String.Concat(AppDir, "\\", "employees.txt");
            string json = File.ReadAllText(filename);
            var tempEmployeeJson = JsonConvert.DeserializeObject<List<Employee>>(json);

            return tempEmployeeJson.Where(x => x.EmployeeIdNo == id).FirstOrDefault();
        }

        public void Delete(string IdNo)
        {
            String filename = String.Concat(AppDir, "\\", "employees.txt");
            string json = File.ReadAllText(filename);
            var tempEmployeeJson = JsonConvert.DeserializeObject<List<Employee>>(json);

            var deleteEmp = tempEmployeeJson.Where(x => x.EmployeeIdNo == IdNo).FirstOrDefault();
            tempEmployeeJson.Remove(deleteEmp);

            string newJson = JsonConvert.SerializeObject(tempEmployeeJson);
            File.WriteAllText(filename, newJson);
        }

        public void Update(EmployeeViewModel tempEmployee)
        {
            String filename = String.Concat(AppDir, "\\", "employees.txt");
            string json = File.ReadAllText(filename);
            var tempEmployeeJson = JsonConvert.DeserializeObject<List<Employee>>(json);

            var entity = tempEmployeeJson.Where(x => x.EmployeeIdNo == tempEmployee.EmployeeIdNo).FirstOrDefault();
            if(entity != null)
            {
                entity.EmployeeName = tempEmployee.EmployeeName;
                entity.EmployeeTIN = tempEmployee.EmployeeTIN;
                entity.EmployeeType = (int)Enum.Parse(typeof(EmployeeTypeNames), tempEmployee.EmployeeType);
                entity.EmployeeBirthdate = DateTime.Parse(tempEmployee.EmployeeBirthdate);
                entity.EmployeeAbsences = Convert.ToDecimal(tempEmployee.EmployeeAbsences);
                entity.EmployeeWorkDays = Convert.ToDecimal(tempEmployee.EmployeeWorkDays);

                decimal divisor = 22.0m;
                decimal dailyrate = Math.Round(Convert.ToDecimal(tempEmployee.EmployeeGrossSalary) / divisor, 2, MidpointRounding.ToEven);

                entity.EmployeeDailyRate = dailyrate;
                entity.EmployeeGrossSalary = Math.Round(Convert.ToDecimal(tempEmployee.EmployeeGrossSalary), 2);
                entity.EmployeeTaxRate = Math.Round(Convert.ToDecimal(tempEmployee.EmployeeTaxRate), 2);
            }

            string newJson = JsonConvert.SerializeObject(tempEmployeeJson);
            File.WriteAllText(filename, String.Empty);
            File.WriteAllText(filename, newJson);
        }

        
    }
}
