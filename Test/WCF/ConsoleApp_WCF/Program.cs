using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleApp_WCF.WCF.UserService;

namespace ConsoleApp_WCF {
    class Program {
        static void Main(string[] args) {

            using (WCF.UserService.UserServiceClient client = new UserServiceClient()) {
                bool success = client.Insert(new User_MODEL());
                Console.WriteLine(success);
            }

            Console.Read();
        }
    }
}
