using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using KANG.MODEL.Enum;

namespace KANG.DBFactory {
    public class DalRepository {
        public static MODEL.Enum.DBEnum DataBaseType = MODEL.Enum.DBEnum.SqlServer;
        public static void Create(string dalName) {
            switch (DataBaseType) {
                case DBEnum.SqlServer:
                    var asm = Assembly.Load("KANG.DAL");
                    asm.CreateInstance("");
                    break;
                case DBEnum.MySql: 
                    break;
                default:
                     
                    break;
            }
        }
    }
}
