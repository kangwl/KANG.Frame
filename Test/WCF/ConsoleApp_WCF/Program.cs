using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleApp_WCF.UserServ;

namespace ConsoleApp_WCF {
    class Program {
        static void Main(string[] args) {
            UserServ.UserOperateOf_User_MODELClient userOperate = new UserOperateOf_User_MODELClient();
            bool success = userOperate.Insert(new User_MODEL() {Name = "sd"});
            Console.WriteLine(success);
            Console.Read();
        }
    }
}
