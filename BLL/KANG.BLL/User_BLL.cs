using KANG.IDAL;
using KANG.MODEL.Enum; 

namespace KANG.BLL {

    public class User_BLL {

        public static MODEL.Enum.DBEnum DataBaseType = MODEL.Enum.DBEnum.SqlServer;

        private static IUserOperate<MODEL.User_MODEL> _userOperate = null;

        private static IUserOperate<MODEL.User_MODEL> userOperate
        {
            get
            {
                switch (DataBaseType) {
                        case DBEnum.SqlServer:
                        _userOperate = new KANG.DAL.User_DAL();
                        break;
                        case DBEnum.MySql:
                        _userOperate = new KANG.MySql.DAL.User_DAL();
                        break;
                    default:
                        _userOperate = new KANG.DAL.User_DAL();
                        break;
                }
                return _userOperate;
            }
        }

        private void GetDal() {
            
           // DBFactory.DalRepository.Create(KANG.DAL.User_DAL);
        }
        public static bool Insert(KANG.MODEL.User_MODEL userModel) {
            return userOperate.Insert(userModel);
        }


    }
}
