using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using KANG.BLL;
using KANG.DB.Bridge;
using KANG.MODEL;

namespace ConsoleApp.Test {
    class Program {
        static void Main(string[] args) {
           // bool success = User_BLL.Insert(new User_MODEL() {Name = "kangwl", Age = 22});
            //bool success = User_BLL.Update(new User_MODEL() {ID = 1,Name = "k", Age = 111});
          //  bool success = User_BLL.UpdateName(1, "kangwl");
          //  Console.WriteLine(success);
            //User_MODEL userModel = new User_MODEL() {ID = 1, Name = "kangwl", Age = 111};
            //userModel.Name = "kwl";
            //userModel.Age = 12;
           // bool success = User_BLL.UpdateName(userModel);
            //Console.WriteLine(success);
            
           // List<User_MODEL> rows = User_BLL.GetList();
           // rows.ForEach(one => Console.WriteLine(one.Name));
            //DataTable dt = User_BLL.GetDataTable("[Name]");
           // bool success = User_BLL.Delete(new User_MODEL() {ID = 2});
            //Console.WriteLine(HttpServerUtility.UrlTokenEncode(Encoding.UTF8.GetBytes("康文立")));
            //Console.WriteLine(Encoding.UTF8.GetString(HttpServerUtility.UrlTokenDecode("5bq35paH56uL0")));
            //int count = User_BLL.GetRecordCount();
            //Console.WriteLine(count);
            //Task task = Task.Factory.StartNew(() => { printNum(); });
            //Console.WriteLine(task.IsCompleted);

            //Console.WriteLine(ctest.GetAwaiter().GetResult());
            Where objWhere = new Where();
            List<Where.Item> items = new List<Where.Item>();
            items.Add(new Where.Item("ID", "LIKE", "%1%"));
            items.Add(new Where.Item("Name", "LIKE", "%k%"));
            items.Add(new Where.Item("Age", "=", 111));
            objWhere.AddRange(items);
            var list = User_BLL.GetList(objWhere);
            Console.WriteLine(list.Count);
            Console.WriteLine(list[0].Name);
            Console.ReadLine();
        }

        public static void printNum() {
            Console.WriteLine(123);
        }



     
    }
}
