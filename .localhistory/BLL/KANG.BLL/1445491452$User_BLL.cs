using KANG.BLL.DALMap;

namespace KANG.BLL {

    public class User_BLL {

        private static IDAL.IUser<MODEL.User_MODEL> UserOperate
        {
            get { return DalRepository.UserDal; }
        }

        public static bool Insert(KANG.MODEL.User_MODEL userModel) {
            return UserOperate.Insert(userModel);
        }

        public static bool Update(KANG.MODEL.User_MODEL userModel) {
            return UserOperate.Update(userModel);
        }


    }

 
}
