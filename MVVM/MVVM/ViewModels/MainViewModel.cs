using MVVM.Models;
using MVVM.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MVVM.ViewModels
{
    class MainViewModel : INotifyPropertyChanged
    {

        Lesson7Model db;
        RelayCommand addEmpCommand;
        RelayCommand addDepCommand;
        RelayCommand editEmpCommand;
        RelayCommand editDepCommand;
        RelayCommand deleteEmpCommand;
        RelayCommand deleteDepCommand;
        IEnumerable<Employee> employees;
        IEnumerable<Employee> allEmployees;
        IEnumerable<Department> departments;
        private Employee selectedEmployee;
        private Department selectedDepartment;
        private int indexSelDep;

        public Employee SelectedEmployee
        {
            get { return selectedEmployee; }
            set
            {
                selectedEmployee = value;
                OnPropertyChanged();
            }
        }
        public Department SelectedDepartment
        {
            get { return selectedDepartment; }
            set
            {
                selectedDepartment = value;
                OnPropertyChanged();
                DGLoadItems();
            }
        }

        

        public int IndexSelDep
        {
            get { return indexSelDep; }
            set
            {
                indexSelDep = value;
                OnPropertyChanged();
            }
        }

        public IEnumerable<Employee> Employees
        {
            get { return employees; }
            set
            {
                employees = value;
                OnPropertyChanged();
            }
        }
        public IEnumerable<Employee> AllEmployees
        {
            get { return allEmployees; }
            set
            {
                allEmployees = value;
                OnPropertyChanged();
            }
        }
        public IEnumerable<Department> Departments
        {
            get { return departments; }
            set
            {
                departments = value;
                OnPropertyChanged();
            }
        }

        public MainViewModel()
        {
            db = new Lesson7Model();
            db.Employees.Load();
            AllEmployees = db.Employees.Local.ToBindingList();
            db.Departments.Load();
            Departments = db.Departments.Local.ToBindingList();
        }
        // команды добавления
        public RelayCommand AddEmpCommand
        {
            get
            {
                return addEmpCommand ??
                  (addEmpCommand = new RelayCommand((o) =>
                  {
                      EmployeeWindow employeeWindow = new EmployeeWindow(new Employee(), IndexSelDep);
                      employeeWindow.cbDepartmentId.ItemsSource = Departments;
                      if (employeeWindow.ShowDialog() == true)
                      {
                          Employee employee = employeeWindow.Employee;
                          employee.DepartmentId = (employeeWindow.cbDepartmentId.SelectedItem as Department).Id;
                          db.Employees.Add(employee);
                          db.SaveChanges();
                          DGLoadItems();
                      }
                  },
                  (o) => SelectedDepartment != null));
            }
        }
        public RelayCommand AddDepCommand
        {
            get
            {
                return addDepCommand ??
                  (addDepCommand = new RelayCommand((o) =>
                  {
                      DepartmentWindow departmentWindow = new DepartmentWindow(new Department());
                      if (departmentWindow.ShowDialog() == true)
                      {
                          Department department = departmentWindow.Department;
                          db.Departments.Add(department);
                          db.SaveChanges();
                      }
                  }));
            }
        }
        // команды редактирования
        public RelayCommand EditEmpCommand
        {
            get
            {
                return editEmpCommand ??
                  (editEmpCommand = new RelayCommand((o) =>
                  {
                      Employee vm = new Employee()
                      {
                          Id = SelectedEmployee.Id,
                          FirstName = SelectedEmployee.FirstName,
                          LastName = SelectedEmployee.LastName,
                          Age = SelectedEmployee.Age,
                          DepartmentId = SelectedEmployee.DepartmentId
                      };
                      EmployeeWindow employeeWindow = new EmployeeWindow(vm, IndexSelDep);
                      employeeWindow.cbDepartmentId.ItemsSource = Departments;
                      if (employeeWindow.ShowDialog() == true)
                      {
                          SelectedEmployee = db.Employees.Find(employeeWindow.Employee.Id);
                          if (SelectedEmployee != null)
                          {
                              SelectedEmployee.FirstName = employeeWindow.Employee.FirstName;
                              SelectedEmployee.LastName = employeeWindow.Employee.LastName;
                              SelectedEmployee.Age = employeeWindow.Employee.Age;
                              SelectedEmployee.DepartmentId = (employeeWindow.cbDepartmentId.SelectedItem as Department).Id;
                              db.Entry(SelectedEmployee).State = EntityState.Modified;
                              db.SaveChanges();
                              DGLoadItems();
                          }
                      }
                  },
                  (o) => SelectedEmployee != null));
            }
        }
        public RelayCommand EditDepCommand
        {
            get
            {
                return editDepCommand ??
                  (editDepCommand = new RelayCommand((o) =>
                  {
                      Department vm = new Department()
                      {
                          Id = SelectedDepartment.Id,
                          Title = SelectedDepartment.Title
                      };
                      DepartmentWindow departmentWindow = new DepartmentWindow(vm);
                      if (departmentWindow.ShowDialog() == true)
                      {
                          SelectedDepartment = db.Departments.Find(departmentWindow.Department.Id);
                          if (SelectedDepartment != null)
                          {
                              SelectedDepartment.Title = departmentWindow.Department.Title;
                              db.Entry(SelectedDepartment).State = EntityState.Modified;
                              db.SaveChanges();
                          }
                      }
                  },
                  (o) => SelectedDepartment != null));
            }
        }
        // команды удаления
        public RelayCommand DeleteEmpCommand
        {
            get
            {
                return deleteEmpCommand ??
                  (deleteEmpCommand = new RelayCommand((o) =>
                  {
                      if (MessageBox.Show("Вы уверены, что хотите удалить всю информацию о сотруднике?",
                                            "Подтверждение", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                      {
                          db.Employees.Remove(SelectedEmployee);
                          db.SaveChanges();
                          DGLoadItems();
                      }
                      else return;
                  },
                  (o) => SelectedEmployee != null));
            }
        }
        public RelayCommand DeleteDepCommand
        {
            get
            {
                return deleteDepCommand ??
                  (deleteDepCommand = new RelayCommand((o) =>
                  {
                      if (MessageBox.Show("Вы уверены, что хотите удалить всю информацию о департаменте?",
                                            "Подтверждение", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                      {
                          db.Departments.Remove(SelectedDepartment);
                          db.SaveChanges();
                          SelectedDepartment = Departments.FirstOrDefault();
                          DGLoadItems();
                      }
                      else return;
                  },
                  (o) => SelectedDepartment != null));
            }
        }
        private void DGLoadItems() => Employees = AllEmployees.Where(emp => emp.DepartmentId == selectedDepartment?.Id).ToList();

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
