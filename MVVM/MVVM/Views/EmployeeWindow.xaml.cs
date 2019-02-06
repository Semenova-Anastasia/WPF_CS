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

        public EmployeeWindow(Employee emp)
        {
            InitializeComponent();
            Employee = emp;
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
    }
}
