using System;
using KANG.IDAL;

namespace KANG.BLL.DALMap {

    /// <summary>
    /// 生成DAL的操作对象
    /// 可更换数据库
    /// </summary>
    public partial class DalRepository {
        /// <summary>
        /// 创建操作实例
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        private static T CreateInstance<T>() where T : class, new() {
            return new T();
        }


        private static IUser operateUser;

        public static IUser OperateUser
        {
            //get { return userOperate ?? (userOperate = CreateInstance<KANG.MySqlDAL.User_DAL>()); }
            //get { return userOperate ?? (userOperate = CreateInstance<KANG.MySqlDAL.User_DAL>()); }
            get { return operateUser ?? (operateUser = CreateInstance<KANG.EFDAL.User_DAL>()); }
        }

        private static ICourse operateCourse;

        public static ICourse OperateCourse
        {
            get { return operateCourse ?? (operateCourse = CreateInstance<EFDAL.Course_DAL>()); }
        }



    }
}
