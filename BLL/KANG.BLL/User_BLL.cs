using KANG.BLL.Map;
using KANG.IDAL;
using KANG.MODEL.Enum;

namespace KANG.BLL {

    public class User_BLL {

        public static IDAL.IUserOperate<MODEL.User_MODEL> UserOperate
        {
            get { return DalRepository.UserDal; }
        }

        public static bool Insert(KANG.MODEL.User_MODEL userModel) {
            return UserOperate.Insert(userModel);
        }


    }

 
}
