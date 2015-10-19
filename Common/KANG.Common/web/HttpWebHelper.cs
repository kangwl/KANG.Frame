#region

using System;
using System.IO;
using System.Net;
using System.Web;

#endregion

namespace KANG.Common.web {
    public class HttpWebHelper {
        public HttpWebHelper(string reqUrl) {
            ReqUrl = reqUrl;
        }

        public HttpWebHelper(string proxyAddr, string proxyUserName, string ProxyUserPass) {
            MyHttpWebRequest.Proxy = CreateWebProxy(proxyAddr, proxyUserName, ProxyUserPass);
        }

        public string ReqUrl { get; set; }

        private HttpWebRequest MyHttpWebRequest {
            get { return WebRequest.Create(ReqUrl) as HttpWebRequest; }
        }

        public WebProxy WebProxy { get; set; }

        public HttpWebResponse GetHttpWebRes() {
            if (MyHttpWebRequest == null) return null;
            return MyHttpWebRequest.GetResponse() as HttpWebResponse;
        }

        public Stream GetResponseStream() {
            var webResponse = GetHttpWebRes();
            if (webResponse == null) return null;
            return webResponse.GetResponseStream();
        }

        public string GetResponseStr() {
            using (var stream = GetResponseStream()) {
                using (var streamReader = new StreamReader(stream)) {
                    var str = streamReader.ReadToEnd();
                    return str;
                }
            }
        }

        /// <summary>
        ///     创建代理
        /// </summary>
        /// <param name="proxyAddr"></param>
        /// <param name="proxyUserName"></param>
        /// <param name="proxyUserPass"></param>
        /// <returns></returns>
        private WebProxy CreateWebProxy(string proxyAddr, string proxyUserName, string proxyUserPass) {
            var webProxy = new WebProxy {
                Address = new Uri(proxyAddr),
                BypassProxyOnLocal = true,
                Credentials = new NetworkCredential(proxyUserName, proxyUserPass),
                UseDefaultCredentials = true
            };
            return webProxy;
        }

        public static string GetIPAddress() {
            string user_IP;
            if (HttpContext.Current == null) return "";
            if (HttpContext.Current.Request.ServerVariables["HTTP_VIA"] != null) {
                user_IP = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"] != null
                    ? HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"]
                    : HttpContext.Current.Request.UserHostAddress;
            }
            else {
                user_IP = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
            }
            return user_IP;
        }
    }
}