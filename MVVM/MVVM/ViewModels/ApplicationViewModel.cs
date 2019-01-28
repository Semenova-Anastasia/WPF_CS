using MVVM.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
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
        DataRowView selectedRow;
        Employee rememberEmployee = new Employee();
        Department rememberDepartment = new Department();
        SqlDataAdapter da;
        //private string connectionString =
        //        @"  Data Source=(localdb)\MSSQLLocalDB;
        //            Initial Catalog=Lesson7;
        //            Integrated Security=True;
        //            Pooling=False";

        IFileService fileService;
        IDialogService dialogService;

        public ObservableCollection<Employee> EmployeesDB { get; set; }
        public ObservableCollection<Department> DepartmentsDB { get; set; }
        public IEnumerable<Employee> ListViewSource { get; set; }

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

        // команды добавления нового объекта
        private RelayCommand addEmployee;
        public RelayCommand AddEmployee
        {
            get
            {
                return addEmployee ??
                  (addEmployee = new RelayCommand(obj =>
                  {
                      Employee employee = new Employee(SelectedDepartment.Id);
                      EmployeesDB.Insert(0, employee);
                      SelectedEmployee = employee;
                      LoadData();
                  }));
            }
        }
        private RelayCommand addDepartment;
        public RelayCommand AddDepartment
        {
            get
            {
                return addDepartment ??
                  (addDepartment = new RelayCommand(obj =>
                  {
                      Department department = new Department();
                      DepartmentsDB.Insert(DepartmentsDB.Count, department);
                      SelectedDepartment = department;
                      UpdateDepartments();
                      LoadData();
                  }));
            }
        }

        // команда удаления объекта
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
                      if (obj is Department department)
                      {
                          for (int i = EmployeesDB.Count - 1; i >= 0; i--)
                          {
                              if (EmployeesDB[i].DepartmentId == SelectedDepartment.Id)
                              {
                                  EmployeesDB.RemoveAt(i);
                                  Console.WriteLine(i);
                              }
                          }
                          DepartmentsDB.Remove(department);
                          UpdateDepartments();
                          LoadData();
                      }
                  },
                 (obj) => (EmployeesDB.Count > 0)||(DepartmentsDB.Count > 0)));
            }
        }

        // команда копирования объекта
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

        // команды сохранения изменений объекта
        private RelayCommand saveEmployee;
        public RelayCommand SaveEmployee
        {
            get
            {
                return saveEmployee ??
                    (saveEmployee = new RelayCommand(obj =>
                    {
                        SelectedEmployee.FirstName = RememberEmployee.FirstName;
                        SelectedEmployee.LastName = RememberEmployee.LastName;
                        SelectedEmployee.Age = RememberEmployee.Age;
                        SelectedEmployee.DepartmentId = RememberEmployee.DepartmentId;
                        MessageBox.Show("Изменения сохранены");
                        //LoadData();
                    },
                    (obj) => SelectedEmployee != null));
            }
        }
        private RelayCommand saveDepartment;
        public RelayCommand SaveDepartment
        {
            get
            {
                return saveDepartment ??
                    (saveDepartment = new RelayCommand(obj =>
                    {
                        SelectedDepartment.Title = RememberDepartment.Title;
                        MessageBox.Show("Изменения сохранены");
                        //LoadData();
                    },
                    (obj) => SelectedDepartment != null));
            }
        }

        // команды отмены изменений объекта
        private RelayCommand cancelChangesEmp;
        public RelayCommand CancelChangesEmp
        {
            get
            {
                return cancelChangesEmp ??
                    (cancelChangesEmp = new RelayCommand(obj =>
                    {
                        RememberEmployee.FirstName = SelectedEmployee.FirstName;
                        RememberEmployee.LastName = SelectedEmployee.LastName;
                        RememberEmployee.Age = SelectedEmployee.Age;
                        RememberEmployee.DepartmentId = SelectedEmployee.DepartmentId;
                        MessageBox.Show("Изменения отменены");
                    },
                    (obj) => SelectedEmployee != null));
            }
        }
        private RelayCommand cancelChangesDep;
        public RelayCommand CancelChangesDep
        {
            get
            {
                return cancelChangesDep ??
                    (cancelChangesDep = new RelayCommand(obj =>
                    {
                        RememberDepartment.Title = SelectedDepartment.Title;
                        MessageBox.Show("Изменения отменены");
                    },
                    (obj) => SelectedDepartment != null));
            }
        }
        //команда удаления записи
        private RelayCommand deleteRow;
        public RelayCommand DeleteRow
        {
            get
            {
                return deleteRow ??
                    (deleteRow = new RelayCommand(obj =>
                    {
                        DataRowView rowView = SelectedRow;
                        rowView.Row.Delete();
                        da.Update(DataTable);
                    },
                    (obj) => SelectedRow != null));
            }
        }

        public Employee SelectedEmployee
        {
            get { return selectedEmployee; }
            set
            {
                Remember(value);
                selectedEmployee = value;
                OnPropertyChanged();
            }
        }
        public Department SelectedDepartment
        {
            get { return selectedDepartment; }
            set
            {
                Remember(value);
                selectedDepartment = value;
                OnPropertyChanged();
                LoadData();
            }
        }
        public DataRowView SelectedRow
        {
            get { return selectedRow; }
            set
            {
                selectedRow = value;
                OnPropertyChanged();
            }
        }

        public Employee RememberEmployee
        {
            get { return rememberEmployee; }
            set
            {
                rememberEmployee = value;
                OnPropertyChanged();
            }
        }
        public Department RememberDepartment
        {
            get { return rememberDepartment; }
            set
            {
                rememberDepartment = value;
                OnPropertyChanged();
            }
        }

        public DataTable DataTable { get; set; }

        public ApplicationViewModel()
        {
            this.dialogService = new DefaultDialogService();
            this.fileService = new JsonFileService();

            BindDataGrid();

            EmployeesDB = new ObservableCollection<Employee>
            {
                new Employee {FirstName="Иван", LastName="Иванов", Age=26, DepartmentId=1 },
                new Employee {FirstName="Петр", LastName="Петров", Age=23, DepartmentId=2 },
                new Employee {FirstName="Виктор", LastName="Сидоров", Age=30, DepartmentId=3 },
                new Employee {FirstName="Алексей", LastName="Михайлов", Age=48, DepartmentId=4 }
            };
            DepartmentsDB = new ObservableCollection<Department>
            {
                new Department{Title="Первый", Id=1},
                new Department{Title="Второй", Id=2},
                new Department{Title="Третий", Id=3},
                new Department{Title="Четвертый", Id=4},
            };
        }

        private void BindDataGrid()
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            con.Open();
            SqlCommand cmd = new SqlCommand(@"select * from [Employees]", con);
            //cmd.CommandText = @"select * from [Employees]";
            //cmd.Connection = con;
            da = new SqlDataAdapter();
            da.SelectCommand = cmd;
            DataTable = new DataTable("Employees");
            da.Fill(DataTable);
            cmd = new SqlCommand("DELETE FROM [Employees] WHERE ID = @Id", con);
            da.DeleteCommand = cmd;
        }

        public void Remember(object obj)
        {
            if (obj is Employee emp)
            {
                RememberEmployee.FirstName = emp.FirstName;
                RememberEmployee.LastName = emp.LastName;
                RememberEmployee.Age = emp.Age;
                RememberEmployee.DepartmentId = emp.DepartmentId;
            }
            if(obj is Department dep)
            {
                RememberDepartment.Title = dep.Title;
            }
        }

        private void UpdateDepartments()
        {
            for (int i = 0; i < DepartmentsDB.Count; i++)
            {
                DepartmentsDB[i].Id = i + 1;
            }
        }

        private void LoadData()
        {
            ListViewSource = EmployeesDB.ToList().Where(emp => emp.DepartmentId == SelectedDepartment?.Id).ToList();
            OnPropertyChanged(nameof(ListViewSource));
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
