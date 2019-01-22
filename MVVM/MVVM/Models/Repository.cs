using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MVVM.Models
{
    public class Repository : INotifyPropertyChanged
    {
        public ObservableCollection<Employee> Employees { get; set; }
        public ObservableCollection<Department> Departments { get; set; }

        public Repository()
        {
            // данные по умлолчанию
            Employees = new ObservableCollection<Employee>
            {
                new Employee {FirstName="Иван", LastName="Иванов", Age=26, DepartmentId=1 },
                new Employee {FirstName="Петр", LastName="Петров", Age=23, DepartmentId=2 },
                new Employee {FirstName="Виктор", LastName="Сидоров", Age=30, DepartmentId=3 },
                new Employee {FirstName="Алексей", LastName="Михайлов", Age=48, DepartmentId=4 }
            };
            Departments = new ObservableCollection<Department>
            {
                new Department{Id=1},
                new Department{Id=2},
                new Department{Id=3},
                new Department{Id=4},
            };
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
