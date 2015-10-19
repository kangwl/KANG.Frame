#region

using System;

#endregion

namespace KANG.Common {
    public static class MyEnum {
        /// <summary>
        ///     获取枚举的值
        /// </summary>
        /// <param name="thEnum"></param>
        /// <returns></returns>
        public static int Val(this Enum thEnum) {
            return Convert.ToInt32(thEnum);
        }
    }
}