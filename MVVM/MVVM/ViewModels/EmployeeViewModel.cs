using MVVM.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MVVM.ViewModels
{
    public class EmployeeViewModel : INotifyPropertyChanged
    {
        private Employee rememberEmployee = new Employee();
        public Employee RememberEmployee
        {
            get { return rememberEmployee; }
            set
            {
                rememberEmployee = value;
                OnPropertyChanged();
            }
        }

        private Employee employee;
        public Employee Employee
        {
            get { return employee; }
            set
            {
                employee = value;
                OnPropertyChanged();
            }
        }
        
        public string FirstName
        {
            get { return employee.FirstName; }
            set
            {
                employee.FirstName = value;
                OnPropertyChanged();
            }
        }
        public string LastName
        {
            get { return employee.LastName; }
            set
            {
                employee.LastName = value;
                OnPropertyChanged();
            }
        }
        public int Age
        {
            get { return employee.Age; }
            set
            {
                employee.Age = value;
                OnPropertyChanged();
            }
        }
        public int DepartmentId
        {
            get { return employee.DepartmentId; }
            set
            {
                employee.DepartmentId = value;
                OnPropertyChanged();
            }
        }

        public EmployeeViewModel(Employee emp)
        {
            employee = emp;
            Remember(emp);
        }

        public void Remember(Employee emp)
        {
            RememberEmployee.FirstName = emp.FirstName;
            RememberEmployee.LastName = emp.LastName;
            RememberEmployee.Age = emp.Age;
            RememberEmployee.DepartmentId = emp.DepartmentId;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
