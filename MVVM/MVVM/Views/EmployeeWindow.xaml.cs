using MVVM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace MVVM.Views
{
    /// <summary>
    /// Interaction logic for EmployeeWindow.xaml
    /// </summary>
    public partial class EmployeeWindow : Window
    {
        public Employee Employee { get; private set; }
        public int IndexDep { get; set; }

        public EmployeeWindow(Employee emp, int iDep)
        {
            InitializeComponent();
            Employee = emp;
            Console.WriteLine("emp.DepartmentId = " + emp.DepartmentId + "   iDep = " + iDep);
            cbDepartmentId.SelectedIndex = iDep - 1;
            this.DataContext = Employee;
        }

        private void Accept_Click(object sender, RoutedEventArgs e)
        {
            if ((tbFirstName.Text != null) && (tbLastName.Text != null) && (tbAge.Text != "0"))
            {
                this.DialogResult = true;
            }
            else MessageBox.Show("Проверьте, все ли поля заполнены правильно.", "Предупреждение", MessageBoxButton.OK);
        }

        private void cbDepartmentId_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            IndexDep = cbDepartmentId.SelectedIndex;
        }
    }
}
