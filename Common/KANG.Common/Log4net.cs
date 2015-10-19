using System;
using System.IO;
using System.Web;
using log4net;

namespace KANG.Common {
    /// <summary>
    /// 需要在global初始化
    /// </summary>
    public class Log4net {

        private Log4net(){}
        static Log4net(){}

        private static readonly log4net.ILog log = log4net.LogManager.GetLogger("mylogger");

        /// <summary>
        /// 初始化log4net
        /// </summary>
        public static void Init(string configXml = "/Config/log4net_n.xml") {
            string log4config_xml = HttpContext.Current.Server.MapPath(configXml);
            if (!File.Exists(log4config_xml)) {
                throw new FileNotFoundException("log4net的配置文件xml不存在", "/Config/log4net_n.xml");
            }
            log4net.Config.XmlConfigurator.Configure(new System.IO.FileInfo(log4config_xml));
        }

        private static ILog Logger {
            get {
                return log;
            }
        }

        public static void Debug(dynamic msg) {
            Logger.Debug(msg);
        }
        public static void Debug(dynamic msg, Exception ex) {
            Logger.Debug(msg, ex);
        }

        public static void Info(dynamic msg) {
            Logger.Info(msg);
        }

        public static void Info(dynamic msg,Exception ex) {
            Logger.Info(msg, ex);
        }

        public static void Warn(dynamic msg) {
            Logger.Warn(msg);
        }
        public static void Warn(dynamic msg, Exception ex) {
            Logger.Warn(msg, ex);
        }

        public static void Error(dynamic msg) {
            Logger.Error(msg);
        }

        public static void Error(dynamic msg,Exception ex) {
            Logger.Error(msg, ex);
        }
    }
}