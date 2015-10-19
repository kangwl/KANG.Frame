#region

using System.Security.Principal;

#endregion

namespace KANG.Common {
    /// <summary>
    ///     OS
    /// </summary>
    public static class CheckOSAdmin {
        /// <summary>
        ///     检查系统是否是以管理员身份运行
        /// </summary>
        /// <returns></returns>
        public static bool IsAdmin() {
            var identity = WindowsIdentity.GetCurrent();
            if (identity != null) {
                var principal = new WindowsPrincipal(identity);
                if (principal.IsInRole(WindowsBuiltInRole.Administrator)) {
                    return true;
                }
                // MessageBox.Show("请以管理员身份运行!");
                return false;
            }
            return false;
        }
    }
}