using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace List_of_employees
{
    class Employee
    {
        public string FirstName { get; private set; }

        public string LastName { get; private set; }

        public int Age { get; private set; }

        public Employee()
        {
        }

        public Employee(string firstName, string lastName, int age)
        {
            FirstName = firstName;
            LastName = lastName;
            Age = age;
        }
    }
}
