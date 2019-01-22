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

        public Repository data { get; set; }
        public ObservableCollection<Employee> ListBoxSource { get; set; }

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
                              fileService.Save(dialogService.FilePath, data.Employees.ToList());
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
                              data.Employees.Clear();
                              foreach (var emp in employees)
                                  data.Employees.Add(emp);
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
                      Employee employee = new Employee();
                      data.Employees.Insert(0, employee);
                      SelectedEmployee = employee;
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
                          data.Employees.Remove(employee);
                      }
                  },
                 (obj) => data.Employees.Count > 0));
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
                          data.Employees.Insert(0, employeeCopy);
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
                    (obj) => data.Employees.Count > 0));
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

            data = new Repository();
            
        }
        private void LoadData()
        {
            ListBoxSource = data.Employees.Where(emp => emp.DepartmentId == SelectedDepartment?.Id);
            OnPropertyChanged(nameof(ListBoxSource));
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
