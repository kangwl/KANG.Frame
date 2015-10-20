using KANG.IDAL;

namespace KANG.BLL.Map {
    /// <summary>
    /// 生成DAL的操作
    /// 可更换数据库
    /// </summary>
    public class DalRepository {


        private static IUserOperate<MODEL.User_MODEL> userOperate;

        public static IUserOperate<MODEL.User_MODEL> UserDal
        {
            //get { return userOperate ?? (userOperate = CreateInstance<KANG.MySqlDAL.User_DAL>()); }
            get { return userOperate ?? (userOperate = CreateInstance<KANG.MySqlDAL.User_DAL>()); }
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
