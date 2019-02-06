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
    /// Interaction logic for DepartmentWindow.xaml
    /// </summary>
    public partial class DepartmentWindow : Window
    {
        public Department Department { get; private set; }

        public DepartmentWindow(Department dep)
        {
            InitializeComponent();
            Department = dep;
            this.DataContext = Department;
        }

        private void Accept_Click(object sender, RoutedEventArgs e)
        {
            if (tbTitle.Text != null)
            {
                this.DialogResult = true;
            }
            else MessageBox.Show("Проверьте, правильно ли заполнено поле.", "Предупреждение", MessageBoxButton.OK);
        }
    }
}
