﻿using KANG.BLL.Map;

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