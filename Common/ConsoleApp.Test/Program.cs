using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Xml;
using KANG.BLL;
using KANG.Common;
using KANG.Common.tools;
using KANG.DB.Bridge;
using KANG.MODEL;

namespace ConsoleApp.Test {
    class Program {
        private static void Main(string[] args) {
            //bool success = User_BLL.Insert(new User_MODEL() {Name = "kangwl", Age = 22});
            //bool success = User_BLL.Update(new User_MODEL() {ID = 1,Name = "k", Age = 111});
            //bool success = User_BLL.UpdateName(1, "kangwl");
            //Console.WriteLine(success);
            //User_MODEL userModel = new User_MODEL() {ID = 1, Name = "kangwl", Age = 111};
            //userModel.Name = "kwl";
            //userModel.Age = 12;
            //bool success = User_BLL.UpdateName(userModel);
            //Console.WriteLine(success);

            //List<User_MODEL> rows = User_BLL.GetList();
            //rows.ForEach(one => Console.WriteLine(one.Name));
            //DataTable dt = User_BLL.GetDataTable("[Name]");
            //bool success = User_BLL.Delete(new User_MODEL() {ID = 2});
            //Console.WriteLine(HttpServerUtility.UrlTokenEncode(Encoding.UTF8.GetBytes("康文立")));
            //Console.WriteLine(Encoding.UTF8.GetString(HttpServerUtility.UrlTokenDecode("5bq35paH56uL0")));
            //int count = User_BLL.GetRecordCount();
            //Console.WriteLine(count);
            //Task task = Task.Factory.StartNew(() => { printNum(); });
            //Console.WriteLine(task.IsCompleted);

            //Console.WriteLine(ctest.GetAwaiter().GetResult());
            //Where objWhere = new Where();
            //List<Where.Item> items = new List<Where.Item>();
            //items.Add(new Where.Item("ID", "LIKE", "%1%"));
            //items.Add(new Where.Item("Name", "LIKE", "%k%"));
            //items.Add(new Where.Item("Age", "=", 111));
            //objWhere.AddRange(items);
            //var list = User_BLL.GetList(objWhere);
            //Console.WriteLine(list.Count);
            //Console.WriteLine(list[0].Name);
            //Console.ReadLine();

            //   Action<int> act = num => { Console.WriteLine(num); };

            // act = printNum;

            //Console.WriteLine("{0},{1}", DateTime.Today,long.MaxValue);
            //Console.WriteLine("{0},{1}", DateTime.MinValue, long.MinValue);

            //Parallel.For(1, 101, (i, state) => {
            //    if (!state.IsExceptional) {

            //        Console.WriteLine(i);
            //    }
            //});

            //List<KANG.MODEL.User_MODEL> users = KANG.BLL.User_BLL.GetList();
            //users.ForEach(user => Console.WriteLine(user.Name));
            
            //Console.ForegroundColor = ConsoleColor.White;
            //Console.Write("hello ");
            //Console.ForegroundColor = (ConsoleColor)10;
            //Console.Write("kwl");
            //Console.WriteLine();
            //Console.ForegroundColor = ConsoleColor.Yellow;
            //Console.WriteLine("nice too meet you");

            //int max = 10;
            //List<int> jList = new List<int>();
            //List<int> oList = new List<int>();
            //for (int i = 0; i < max; i++) {
            //    if (i%2 == 1) {
            //        jList.Add(i); 
            //    }
            //    else if(i % 2 == 0) {
            //        oList.Add(i); 
            //    }
            //}
            //Console.WriteLine(jList.Count + oList.Count);

            //List<string> list = new List<string>() {
            //    "kwl",
            //    "k",
            //    "qwerty"
            //};
            //string str = "kangwl";
            //str.ToCharArray().ToList().ForEach(Console.WriteLine);

            //Student s = new Student();
            //s.Age = 23;
            //s.Name = "kwlss";
            //while (true) {
            //    Console.WriteLine("请输入网址：");
            //    string url = Console.ReadLine();
            //    MyWebReq webReq = new MyWebReq();
            //    string html = webReq.GetWebHtml(string.Format("http://{0}/", url));
            //    Console.WriteLine(html);
            //}

            Guid guid = IdGenerator.NewGuidSequentialString();
            Console.WriteLine(IdGenerator.GetTimestampFromGuidSequentialString(guid));
            Console.Read();
        }

        public static void printNum(int num) {
            
            Console.WriteLine(num);
        }

 

    }
    
    public class Student{
        public string Name{get;set;}
        public int Age{get;set;}
    }
}
