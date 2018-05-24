using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LeaveLib.Domain;

namespace LeaveLib.Infra
{
    public class EmployeeRepository
    {
        private static readonly List<Employee> Employees = new List<Employee>();

        public Employee GetById(int id)
        {
            return Employees.SingleOrDefault(s => s.Id == id);
        }

        public List<Employee> GetAll()
        {
            return Employees;
        }

        public void SaveOrUpdate(Employee employee)
        {
            if (employee.Id == 0)
            {
                employee.Id = Employees.Count + 1;
                Employees.Add(employee);
            }
            else
            {
                foreach (Employee t in Employees)
                {
                    if (t.Id != employee.Id)
                        continue;

                    t.FullName = employee.FullName;
                    t.Manager = employee.Manager;
                }
            }

        }

    }
}
