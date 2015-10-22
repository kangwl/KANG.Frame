using System;
using KANG.IDAL;

namespace KANG.BLL.DALMap {

    /// <summary>
    /// 生成DAL的操作
    /// 可更换数据库
    /// </summary>
    public partial class DalRepository {


        private static IUser<MODEL.User_MODEL> userOperate;

        public static IUser<MODEL.User_MODEL> UserDal
        {
            //get { return userOperate ?? (userOperate = CreateInstance<KANG.MySqlDAL.User_DAL>()); }
            //get { return userOperate ?? (userOperate = CreateInstance<KANG.MySqlDAL.User_DAL>()); }
            get { return userOperate ?? (userOperate = CreateInstance<KANG.EFDAL.User_DAL>()); }
        }




        /// <summary>
        /// 创建操作实例
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        private static T CreateInstance<T>() where T : class, new() {
            return new T();
        }
    }
}
