using MVVM.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MVVM.ViewModels
{
    public class ApplicationViewModel : INotifyPropertyChanged
    {
        Employee selectedEmployee;
        Department selectedDepartment;

        EmployeeViewModel editEmployeeVM;
        //DepartmentViewModel editDepartVM;

        IFileService fileService;
        IDialogService dialogService;

        public ObservableCollection<Employee> EmployeesDB { get; set; }
        public ObservableCollection<Department> DepartmentsDB { get; set; }
        public IEnumerable<Employee> ListBoxSource { get; set; }

        // команда сохранения файла
        private RelayCommand saveCommand;
        public RelayCommand SaveCommand
        {
            get
            {
                return saveCommand ??
                  (saveCommand = new RelayCommand(obj =>
                  {
                      try
                      {
                          if (dialogService.SaveFileDialog() == true)
                          {
                              fileService.Save(dialogService.FilePath, EmployeesDB.ToList());
                              dialogService.ShowMessage("Файл сохранен");
                          }
                      }
                      catch (Exception ex)
                      {
                          dialogService.ShowMessage(ex.Message);
                      }
                  }));
            }
        }

        // команда открытия файла
        private RelayCommand openCommand;
        public RelayCommand OpenCommand
        {
            get
            {
                return openCommand ??
                  (openCommand = new RelayCommand(obj =>
                  {
                      try
                      {
                          if (dialogService.OpenFileDialog() == true)
                          {
                              var employees = fileService.Open(dialogService.FilePath);
                              EmployeesDB.Clear();
                              foreach (var emp in employees)
                                  EmployeesDB.Add(emp);
                              dialogService.ShowMessage("Файл открыт");
                          }
                      }
                      catch (Exception ex)
                      {
                          dialogService.ShowMessage(ex.Message);
                      }
                  }));
            }
        }

        // команда добавления нового объекта
        private RelayCommand addCommand;
        public RelayCommand AddCommand
        {
            get
            {
                return addCommand ??
                  (addCommand = new RelayCommand(obj =>
                  {
                      Employee employee = new Employee(SelectedDepartment.Id); 
                      EmployeesDB.Insert(0, employee);
                      SelectedEmployee = employee;
                      EmployeeEditingWindow employeeEditView = new EmployeeEditingWindow();
                      editEmployeeVM = new EmployeeViewModel(selectedEmployee);
                      employeeEditView.DataContext = editEmployeeVM;
                      if (employeeEditView.ShowDialog() == true)
                      {
                          SelectedEmployee = editEmployeeVM.Employee;
                          MessageBox.Show("Изменения сохранены");
                          LoadData();
                      }
                      else
                      {
                          EmployeesDB.Remove(employee);
                          MessageBox.Show("Изменения не сохранены");
                          LoadData();
                      }
                  }));
            }
        }

        private RelayCommand removeCommand;
        public RelayCommand RemoveCommand
        {
            get
            {
                return removeCommand ??
                  (removeCommand = new RelayCommand(obj =>
                  {
                      if (obj is Employee employee)
                      {
                          EmployeesDB.Remove(employee);
                          LoadData();
                      }
                  },
                 (obj) => EmployeesDB.Count > 0));
            }
        }

        private RelayCommand doubleCommand;
        public RelayCommand DoubleCommand
        {
            get
            {
                return doubleCommand ??
                  (doubleCommand = new RelayCommand(obj =>
                  {
                      if (obj is Employee employee)
                      {
                          Employee employeeCopy = new Employee
                          {
                              LastName = employee.LastName,
                              Age = employee.Age,
                              FirstName = employee.FirstName,
                              DepartmentId = employee.DepartmentId
                          };
                          EmployeesDB.Insert(0, employeeCopy);
                          LoadData();
                      }
                  }));
            }
        }

        private RelayCommand editEmployee;
        public RelayCommand EditEmployee
        {
            get
            {
                return editEmployee ??
                    (editEmployee = new RelayCommand(obj =>
                    {
                        if (obj is Employee)
                        {
                            EmployeeEditingWindow employeeEditView = new EmployeeEditingWindow();
                            editEmployeeVM = new EmployeeViewModel(selectedEmployee);
                            employeeEditView.DataContext = editEmployeeVM;
                            if (employeeEditView.ShowDialog() == true)
                            {
                                SelectedEmployee = editEmployeeVM.Employee;
                                MessageBox.Show("Изменения сохранены");
                                LoadData();
                            }
                            else
                            {
                                SelectedEmployee.LastName = editEmployeeVM.RememberEmployee.LastName;
                                SelectedEmployee.Age = editEmployeeVM.RememberEmployee.Age;
                                SelectedEmployee.FirstName = editEmployeeVM.RememberEmployee.FirstName;
                                SelectedEmployee.DepartmentId = editEmployeeVM.RememberEmployee.DepartmentId;
                                MessageBox.Show("Изменения не сохранены");
                            }
                        }
                    },
                    (obj) => EmployeesDB.Count > 0));
            }
        }

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
                LoadData();
            }
        }
        
        public ApplicationViewModel()
        {
            this.dialogService = new DefaultDialogService();
            this.fileService = new JsonFileService();

            EmployeesDB = new ObservableCollection<Employee>
            {
                new Employee {FirstName="Иван", LastName="Иванов", Age=26, DepartmentId=1 },
                new Employee {FirstName="Петр", LastName="Петров", Age=23, DepartmentId=2 },
                new Employee {FirstName="Виктор", LastName="Сидоров", Age=30, DepartmentId=3 },
                new Employee {FirstName="Алексей", LastName="Михайлов", Age=48, DepartmentId=4 }
            };
            DepartmentsDB = new ObservableCollection<Department>
            {
                new Department{Id=1},
                new Department{Id=2},
                new Department{Id=3},
                new Department{Id=4},
            };

        }
        private void LoadData()
        {
            ListBoxSource = EmployeesDB.ToList().Where(emp => emp.DepartmentId == SelectedDepartment?.Id).ToList();
            OnPropertyChanged(nameof(ListBoxSource));
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
