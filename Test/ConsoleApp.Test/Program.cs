using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KANG.BLL;
using KANG.MODEL;

namespace ConsoleApp.Test {
    class Program {
        static void Main(string[] args) {
            KANG.BLL.User_BLL.Insert(new User_MODEL());
        }
    }
}
