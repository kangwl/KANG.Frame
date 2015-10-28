using System.Collections.Generic;
using System.Data;
using KANG.BLL.DALMap;
using KANG.DB.Bridge;

namespace KANG.BLL {
    /// <summary>
    /// user service
    /// </summary>
    public class User_BLL {

        private static IDAL.IUser OperateUser {
            get { return DalRepository.OperateUser; }
        }

        public static bool Insert(KANG.MODEL.User_MODEL userModel) {
            return OperateUser.Insert(userModel);
        }

        public static bool Update(KANG.MODEL.User_MODEL userModel) {
            return OperateUser.Update(userModel);
        }

        public static bool UpdateName(int id,string name) {
            return OperateUser.UpdateName(id, name);
        }

        public static bool UpdateName(MODEL.User_MODEL userModel) {
            return OperateUser.UpdateName(userModel);
        }

        public static List<MODEL.User_MODEL> GetList(Where objWhere = null, string orderField = null, bool asc = true,
            int pageIndex = 0, int pageSize = 10) {
            return OperateUser.GetList(objWhere, orderField, asc, pageIndex, pageSize);
        }

        public static DataTable GetDataTable(string fields) {
            return OperateUser.GetDataTable(fields);
        }

        public static bool Delete(int id) {
            return OperateUser.Delete(id);
        }

        public static bool Delete(MODEL.User_MODEL userModel) {
            return OperateUser.Delete(userModel);
        }

        public static int GetRecordCount(Where objWhere = null) {
            return OperateUser.GetRecordCount(objWhere);
        }

        public static bool Exist(Where objWhere) {
            return OperateUser.GetRecordCount(objWhere) > 0;
        }

    }

 
}
