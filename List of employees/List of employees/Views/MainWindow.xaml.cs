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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Collections.ObjectModel;

namespace List_of_employees
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //Binding bs = new Binding();
        ObservableCollection<Department> database = new ObservableCollection<Department>();
        public MainWindow()
        {
            InitializeComponent();
            Loaded += MainWindow_Loaded;
            btnAddEmp.Click += delegate { database[0].AddEmp(tbFirstName.Text,tbLastName.Text, Convert.ToInt32(tbAge.Text)); };
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            //lstEmloyees.ItemsSource = database.employees;
            //lstEmloyees.ItemStringFormat = $"{database.employees[0].LastName} {database.employees[0].FirstName}";
        }
    }
}
