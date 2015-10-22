using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KANG.BLL;
using KANG.MODEL;

namespace ConsoleApp.Test {
    class Program {
        static void Main(string[] args) {
           // bool success = User_BLL.Insert(new User_MODEL() {Name = "kangwl", Age = 22});
            //bool success = User_BLL.Update(new User_MODEL() {ID = 1,Name = "k", Age = 111});
            //bool success = User_BLL.UpdateName(1, "kangwl");
            //User_MODEL userModel = new User_MODEL() {ID = 1, Name = "kangwl", Age = 111};
            //userModel.Name = "kwl";
            //userModel.Age = 12;
           // bool success = User_BLL.UpdateName(userModel);
            //Console.WriteLine(success);
            
           // List<User_MODEL> rows = User_BLL.GetList();
           // rows.ForEach(one => Console.WriteLine(one.Name));
            //DataTable dt = User_BLL.GetDataTable("[Name]");
           // bool success = User_BLL.Delete(new User_MODEL() {ID = 2});
            int count = User_BLL.GetRecordCount();
            Console.WriteLine(count);
            Console.ReadLine();
        }

     
    }
}
