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

        private static log4net.ILog log;
        /// <summary>
        /// 初始化log4net
        /// </summary>
        public static void Init(string loggerName, string configXml) {
            log = log4net.LogManager.GetLogger(loggerName);
            string log4config_xml = "";
            log4config_xml = configXml.Contains(":") ? configXml : HttpContext.Current.Server.MapPath(configXml);
            if (!File.Exists(log4config_xml)) {
                throw new FileNotFoundException("log4net的配置文件xml不存在", "/Config/log4net_n.xml");
            }
            log4net.Config.XmlConfigurator.Configure(new System.IO.FileInfo(log4config_xml));
        }

        /// <summary>
        /// 初始化log4net
        /// </summary>
        public static void Init(string configXml = "/Config/log4net_n.xml") {
           
            log = log4net.LogManager.GetLogger("mylogger");
            string log4config_xml = "";
            log4config_xml = configXml.Contains(":") ? configXml : HttpContext.Current.Server.MapPath(configXml);
            if (!File.Exists(log4config_xml)) {
                throw new FileNotFoundException("log4net的配置文件xml不存在", "/Config/log4net_n.xml");
            }
            log4net.Config.XmlConfigurator.Configure(new System.IO.FileInfo(log4config_xml));
        }


        public static void Debug(dynamic msg) {
            log.Debug(msg);
        }
        public static void Debug(dynamic msg, Exception ex) {
            log.Debug(msg, ex);
        }

        public static void Info(dynamic msg) {
            log.Info(msg);
        }

        public static void Info(dynamic msg,Exception ex) {
            log.Info(msg, ex);
        }

        public static void Warn(dynamic msg) {
            log.Warn(msg);
        }
        public static void Warn(dynamic msg, Exception ex) {
            log.Warn(msg, ex);
        }

        public static void Error(dynamic msg) {
            log.Error(msg);
        }

        public static void Error(dynamic msg,Exception ex) {
            log.Error(msg, ex);
        }
    }
}