using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.InMemory
{
    public class SampleDataAccess
    {
        private readonly IMemoryCache _memoryCache;

        public SampleDataAccess(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }
        public List<EmployeeModel> GetEmployees()
        {
            List<EmployeeModel> employees = new();

            employees.Add(new() { FirstName = "Joe", LastName = "Doe" });
            employees.Add(new() { FirstName = "Sue", LastName = "Storm" });
            employees.Add(new() { FirstName = "John", LastName = "Smith" });
            employees.Add(new() { FirstName = "Jane", LastName = "Jones" });

            //Simulating slow access database
            Thread.Sleep(3000);

            return employees;
        }

        public async Task<List<EmployeeModel>> GetEmployeesAsync()
        {
            List<EmployeeModel> employees = new();

            employees.Add(new() { FirstName = "Joe", LastName = "Doe" });
            employees.Add(new() { FirstName = "Sue", LastName = "Storm" });
            employees.Add(new() { FirstName = "John", LastName = "Smith" });
            employees.Add(new() { FirstName = "Jane", LastName = "Jones" });

            //Simulating slow access database
            await Task.Delay(3000);

            return employees;
        }

        public async Task<List<EmployeeModel>> GetEmployeesCache()
        {
            List<EmployeeModel> employees = new();

            employees = _memoryCache.Get<List<EmployeeModel>>("Employees");

            if (employees is null)
            {
                employees = new();

                employees.Add(new() { FirstName = "Joe", LastName = "Doe" });
                employees.Add(new() { FirstName = "Sue", LastName = "Storm" });
                employees.Add(new() { FirstName = "John", LastName = "Smith" });
                employees.Add(new() { FirstName = "Jane", LastName = "Jones" });

                //Simulating slow access database
                await Task.Delay(millisecondsDelay:3000);

                _memoryCache.Set(key:"Employees", employees, TimeSpan.FromMinutes(5));
            }

            return employees;
        }
    }
}
