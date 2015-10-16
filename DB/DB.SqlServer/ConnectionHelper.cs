namespace DB.SqlServer
{
    public static class ConnectionHelper
    {
        /// <summary>
        /// 连接字符串 web.config配置
        /// </summary>
        public static string ConnectString {
            get {
                return System.Configuration.ConfigurationManager.ConnectionStrings["Conn_NanJingEdu"].ConnectionString;
            }
        }
    }
}
