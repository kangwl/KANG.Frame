using System;
using System.Web;
using System.Web.Security;

namespace KANG.Common {
    public class LoginCookieHelper {
        /// <summary>
        /// 用户登录成功后记录ticket
        /// </summary>
        /// <param name="users_ID">用户id 主键</param>
        /// <param name="userData">用户数据（比如个人权限）</param>
        /// <param name="expireDateTime">过期时间</param>
        public static void RecordLogined(int users_ID, string userData, DateTime expireDateTime) {
            //ticket
            FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(
                1, users_ID.ToString(), DateTime.Now, expireDateTime, false, userData, "/");
            
            //encrypt
            string hashTicket = FormsAuthentication.Encrypt(ticket);
            //create cookie
            HttpCookie userCookie = new HttpCookie(FormsAuthentication.FormsCookieName, hashTicket);
            userCookie.HttpOnly = true;//不允许客户端访问
            userCookie.Expires = ticket.Expiration;
            HttpContext.Current.Response.Cookies.Add(userCookie);
        }

        public static string GetCurrentUser() {
            string userid = HttpContext.Current.User.Identity.Name;

            return userid;
        }

        /// <summary>
        /// 登出
        /// </summary>
        public static void LoginOut() {
            FormsAuthentication.SignOut();
        }

        public static string DefaultUrl
        {
            get { return FormsAuthentication.DefaultUrl; }
        }

        public static string LoginUrl
        {
            get { return FormsAuthentication.LoginUrl; }
        }


    }
}
