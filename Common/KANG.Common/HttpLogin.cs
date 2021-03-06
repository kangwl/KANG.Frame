﻿#region

using System;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Text;
using System.Web;

#endregion

namespace KANG.Common {
    //string userName = "kangwl";
    //string userPwd = "qqq";
    //string content = LogingHelper.Login("https://www.aaaa.com/j_spring_security_check", "j_username=" + userName + "&j_password=" + userPwd,
    //     "https://www.aaaa.com");
    // richTextBox1.Text = content;
    // richTextBox1.Text =
    //     LogingHelper.getPage("https://www.aaaa.com/account/index.action",
    //         "https://www.aaaa.com");
    public class HttpLogin {
        public static CookieContainer theCC = new CookieContainer();

        /// <summary>
        ///     登录方法(无验证码)
        /// </summary>
        /// <PARAM name="url">POST请求的地址</PARAM>
        /// <PARAM name="paramList">参数列表 例如 name=zhangsan&pass=lisi</PARAM>
        /// <PARAM name="referer">来源地址</PARAM>
        /// <RETURNS></RETURNS>
        public static string Login(string url, string paramList, string referer) {
            HttpWebResponse res = null;
            HttpWebRequest req = null;
            var strResult = "";
            try {
                req = (HttpWebRequest) WebRequest.Create(url);
                //配置请求header   
                req.Headers.Add(HttpRequestHeader.AcceptCharset, "GBK,utf-8;q=0.7,*;q=0.3");
                req.Headers.Add(HttpRequestHeader.AcceptEncoding, "gzip,deflate,sdch");
                req.Headers.Add(HttpRequestHeader.AcceptLanguage, "zh-CN,zh;q=0.8");
                req.Accept =
                    "application/xml,application/xhtml+xml,text/html;q=0.9,text/plain;q=0.8,image/png,*/*;q=0.5";
                req.KeepAlive = true;
                req.Referer = referer;
                req.Headers.Add(HttpRequestHeader.CacheControl, "max-age=0");
                req.UserAgent =
                    "Mozilla/5.0 (Windows; U; Windows NT 5.2; en-US) AppleWebKit/534.7 (KHTML, like Gecko) Chrome/7.0.517.5 Safari/534.7";
                req.Method = "POST";
                req.ContentType = "application/x-www-form-urlencoded";
                req.AllowAutoRedirect = true;
                //设置cookieContainer用来接收cookie   
                req.CookieContainer = theCC;
                var UrlEncoded = new StringBuilder();
                //对参数进行encode   
                char[] reserved = {'?', '=', '&'};
                byte[] SomeBytes = null;
                if (paramList != null) {
                    int i = 0, j;
                    while (i < paramList.Length) {
                        j = paramList.IndexOfAny(reserved, i);
                        if (j == -1) {
                            UrlEncoded.Append(HttpUtility.UrlEncode(paramList.Substring(i, paramList.Length - i)));
                            break;
                        }
                        UrlEncoded.Append(HttpUtility.UrlEncode(paramList.Substring(i, j - i)));
                        UrlEncoded.Append(paramList.Substring(j, 1));
                        i = j + 1;
                    }
                    SomeBytes = Encoding.UTF8.GetBytes(UrlEncoded.ToString());
                    req.ContentLength = SomeBytes.Length;
                    var newStream = req.GetRequestStream();
                    newStream.Write(SomeBytes, 0, SomeBytes.Length);
                    newStream.Close();
                }
                else {
                    req.ContentLength = 0;
                }
                //返回请求   
                res = (HttpWebResponse) req.GetResponse();
                var encode = Encoding.GetEncoding("utf-8");
                Stream responseStream = null;
                if (res.ContentEncoding.ToLower() == "gzip") {
                    responseStream = new GZipStream(res.GetResponseStream(),
                        CompressionMode.Decompress);
                }
                else if (res.ContentEncoding.ToLower() == "deflate") {
                    responseStream = new DeflateStream(res.GetResponseStream(),
                        CompressionMode.Decompress);
                }
                else {
                    responseStream = res.GetResponseStream();
                }
                var sr = new StreamReader(responseStream, encode);
                strResult = sr.ReadToEnd();
            }
            catch (Exception e) {
                //writeLog   
            }
            finally {
                res.Close();
            }
            return strResult;
        }

        /// <summary>
        ///     获取页面HTML
        ///     <PARAM name="url"></PARAM>
        ///     <PARAM name="paramList"></PARAM>
        ///     <RETURNS></RETURNS>
        public static string getPage(string url, string referer) {
            var req = (HttpWebRequest) WebRequest.Create(url);
            var strResult = string.Empty;
            req.Headers["If-None-Match"] = "36d0ed736e88c71:d9f";
            req.Referer = referer;
            req.CookieContainer = theCC;
            var res = (HttpWebResponse) req.GetResponse();
            StreamReader sr = null;
            try {
                sr = new StreamReader(res.GetResponseStream(), Encoding.UTF8);
                strResult = sr.ReadToEnd();
            }
            catch (Exception ex) {
                //writeLog   
            }
            finally {
                sr.Close();
            }
            return strResult;
        }

        /// <summary>
        ///     模仿异步请求POST的方法
        /// </summary>
        /// <PARAM name="url"></PARAM>
        /// <PARAM name="referer"></PARAM>
        /// <PARAM name="methed"></PARAM>
        /// <PARAM name="paramList"></PARAM>
        /// <RETURNS></RETURNS>
        public static string VisitPage(string url, string referer, string paramList) {
            HttpWebResponse response = null;
            var strResult = string.Empty;
            try {
                var request = WebRequest.Create(url) as HttpWebRequest;
                request.Method = "POST";
                request.KeepAlive = true;
                request.Referer = referer;
                request.Headers.Add(HttpRequestHeader.AcceptCharset, "GBK,utf-8;q=0.7,*;q=0.3");
                request.Headers.Add(HttpRequestHeader.AcceptEncoding, "gzip,deflate,sdch");
                request.Headers.Add(HttpRequestHeader.AcceptLanguage, "zh-CN,zh;q=0.8");
                request.Accept =
                    "application/xml,application/xhtml+xml,text/html;q=0.9,text/plain;q=0.8,image/png,*/*;q=0.5";
                request.UserAgent =
                    "Mozilla/5.0 (Windows; U; Windows NT 5.2; en-US) AppleWebKit/534.7 (KHTML, like Gecko) Chrome/7.0.517.5 Safari/534.7";
                request.ContentType = "application/x-www-form-urlencoded";
                request.CookieContainer = theCC;
                request.Headers.Add("X-Requested-With", "XMLHttpRequest");
                var UrlEncoded = new StringBuilder();
                //对参数进行encode   
                char[] reserved = {'?', '=', '&'};
                byte[] SomeBytes = null;
                if (paramList != null) {
                    int i = 0, j;
                    while (i < paramList.Length) {
                        j = paramList.IndexOfAny(reserved, i);
                        if (j == -1) {
                            UrlEncoded.Append(HttpUtility.UrlEncode(paramList.Substring(i, paramList.Length - i)));
                            break;
                        }
                        UrlEncoded.Append(HttpUtility.UrlEncode(paramList.Substring(i, j - i)));
                        UrlEncoded.Append(paramList.Substring(j, 1));
                        i = j + 1;
                    }
                    SomeBytes = Encoding.UTF8.GetBytes(UrlEncoded.ToString());
                    request.ContentLength = SomeBytes.Length;
                    var newStream = request.GetRequestStream();
                    newStream.Write(SomeBytes, 0, SomeBytes.Length);
                    newStream.Close();
                }
                response = (HttpWebResponse) request.GetResponse();
                var encode = Encoding.GetEncoding("utf-8");
                Stream responseStream = null;
                if (response.ContentEncoding.ToLower() == "gzip") {
                    responseStream = new GZipStream(response.GetResponseStream(),
                        CompressionMode.Decompress);
                }
                else if (response.ContentEncoding.ToLower() == "deflate") {
                    responseStream = new DeflateStream(response.GetResponseStream(),
                        CompressionMode.Decompress);
                }
                else {
                    responseStream = response.GetResponseStream();
                }
                var sr = new StreamReader(responseStream, encode);
                strResult = sr.ReadToEnd();
            }
            catch {
                //dosomething   
            }
            finally {
                response.Close();
            }
            return strResult;
        }
    }
}