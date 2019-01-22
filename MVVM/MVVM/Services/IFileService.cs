using MVVM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVM
{
    public interface IFileService
    {
        List<Employee> Open(string filename);
        void Save(string filename, List<Employee> employeesList);
    }
}
