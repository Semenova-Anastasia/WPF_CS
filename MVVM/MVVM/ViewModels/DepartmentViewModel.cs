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
    public class DepartmentViewModel : INotifyPropertyChanged
    {
        private Department rememberDepartment = new Department();
        public Department RememberDepartment
        {
            get { return rememberDepartment; }
            set
            {
                rememberDepartment = value;
                OnPropertyChanged();
            }
        }

        private Department department;
        public Department Department
        {
            get { return department; }
            set
            {
                department = value;
                OnPropertyChanged();
            }
        }

        public string Title
        {
            get { return department.Title; }
            set
            {
                department.Title = value;
                OnPropertyChanged();
            }
        }
        
        public int Id
        {
            get { return department.Id; }
            set
            {
                department.Id = value;
                OnPropertyChanged();
            }
        }

        public DepartmentViewModel(Department d)
        {
            department = d;
            Remember(d);
        }

        public void Remember(Department d)
        {
            RememberDepartment.Title = d.Title;
            RememberDepartment.Id = d.Id;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
