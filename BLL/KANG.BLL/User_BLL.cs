using System.Collections.Generic;
using System.Data;
using KANG.BLL.DALMap;
using KANG.DB.Bridge;

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

        public static bool UpdateName(int id,string name) {
            return UserOperate.UpdateName(id, name);
        }

        public static bool UpdateName(MODEL.User_MODEL userModel) {
            return UserOperate.UpdateName(userModel);
        }

        public static List<MODEL.User_MODEL> GetList() {
            return UserOperate.GetList();
        }

        public static DataTable GetDataTable(string fields) {
            return UserOperate.GetDataTable(fields);
        }

        public static bool Delete(int id) {
            return UserOperate.Delete(id);
        }

        public static bool Delete(MODEL.User_MODEL userModel) {
            return UserOperate.Delete(userModel);
        }

        public static int GetRecordCount(Where objWhere = null) {
            return UserOperate.GetRecordCount(objWhere);
        }

        public static bool Exist(Where objWhere) {
            return UserOperate.GetRecordCount(objWhere) > 0;
        }

    }

 
}
