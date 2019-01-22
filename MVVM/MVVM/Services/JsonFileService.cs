using MVVM.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

namespace MVVM
{
    public class JsonFileService : IFileService
    {
        public List<Employee> Open(string filename)
        {
            List<Employee> employees = new List<Employee>();
            DataContractJsonSerializer jsonFormatter =
                new DataContractJsonSerializer(typeof(List<Employee>));
            using (FileStream fs = new FileStream(filename, FileMode.OpenOrCreate))
            {
                employees = jsonFormatter.ReadObject(fs) as List<Employee>;
            }

            return employees;
        }

        public void Save(string filename, List<Employee> employeesList)
        {
            DataContractJsonSerializer jsonFormatter =
                new DataContractJsonSerializer(typeof(List<Employee>));
            using (FileStream fs = new FileStream(filename, FileMode.Create))
            {
                jsonFormatter.WriteObject(fs, employeesList);
            }
        }
    }
}
