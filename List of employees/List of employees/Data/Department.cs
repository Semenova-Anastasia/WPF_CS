using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace List_of_employees
{
    class Department
    {
        public string Name { get; private set; }

        public int Number { get; private set; }

        public ObservableCollection<Employee> employees = new ObservableCollection<Employee>();

        public Department()
        {
        }

        public Department(string name, int number)
        {
            Name = name;
            Number = number;
        }
        public Employee this[int index]
        {
            get { return employees[index]; }
            set { employees[index] = value; }
        }

        public void AddEmp(string firstName, string lastName, int age)
        {
            employees.Add(new Employee(firstName, lastName, age));
        }

        public void Remove(int index)
        {
            if (employees != null && index < employees.Count && index >= 0) employees.RemoveAt(index);
            else throw new IndexOutOfRangeException("Error!");
        }
    }
}
