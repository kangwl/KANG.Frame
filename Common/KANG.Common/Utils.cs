#region

using System;
using System.Data;
using System.IO;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

#endregion

namespace KANG.Common {
    public class Utils {
        /// <summary>
        ///     DataTable的深度拷贝,把SrcTable中的内容复制到DesTable中
        /// </summary>
        /// <param name="SrcTable">源</param>
        /// <param name="DesTable">目的</param>
        /// <returns></returns>
        public static DataTable TableCopy(DataTable SrcTable, out DataTable DesTable) {
            DesTable = new DataTable();
            foreach (DataColumn dc in SrcTable.Columns) {
                DesTable.Columns.Add(dc.ColumnName, dc.DataType);
            }
            foreach (DataRow dr in SrcTable.Rows) {
                var Newdr = DesTable.NewRow();
                foreach (DataColumn newdc in SrcTable.Columns) {
                    Newdr[newdc.ColumnName] = dr[newdc.ColumnName];
                }
                DesTable.Rows.Add(Newdr);
            }
            return DesTable;
        }


        /// <summary>
        ///     根据Url获得源文件内容
        /// </summary>
        /// <param name="url">合法的Url地址</param>
        /// <returns></returns>
        public static string GetSourceTextByUrl(string url) {
            try {
                var request = WebRequest.Create(url);
                request.Timeout = 20000; //20秒超时 
                var response = request.GetResponse();
                var resStream = response.GetResponseStream();
                var sb = new StringBuilder();
                var buffer = new byte[1024];
                var length = 1;
                while (length > 0) {
                    length = resStream.Read(buffer, 0, buffer.Length);
                    sb.Append(GetStringByByte(buffer));
                }
                resStream.Close();
                return sb.ToString();
            }
            catch {
                return "";
            }
        }

        /// <summary>
        ///     把字节变成字符串
        /// </summary>
        /// <param name="temp"></param>
        /// <returns></returns>
        public static string GetStringByByte(byte[] temp) {
            var encoding = Encoding.GetEncoding("GB2312");
            return encoding.GetString(temp);
        }


        /// <summary>
        ///     把字符串变成字节
        /// </summary>
        /// <param name="temp"></param>
        /// <returns></returns>
        public static byte[] GetStringByByte(string temp) {
            var encoding = Encoding.GetEncoding("GB2312");
            return encoding.GetBytes(temp);
        }

        /// <summary>
        ///     从图片地址下载图片到本地磁盘
        /// </summary>
        /// <param name="ToLocalPath">图片本地磁盘地址</param>
        /// <param name="Url">图片网址</param>
        /// <returns></returns>
        public static bool SavePhotoFromUrl(string FileName, string Url) {
            var Value = false;
            WebResponse response = null;
            Stream stream = null;

            try {
                var request = WebRequest.Create(Url);

                response = request.GetResponse();
                stream = response.GetResponseStream();

                if (!response.ContentType.ToLower().StartsWith("text/")) {
                    Value = SaveBinaryFile(response, FileName);
                }
            }
            catch (Exception err) {
                var aa = err.ToString();
            }
            return Value;
        }

        /// <summary>
        ///     Save a binary file to disk.
        /// </summary>
        /// <param name="response">The response used to save the file</param>
        // 将二进制文件保存到磁盘
        public static bool SaveBinaryFile(WebResponse response, string FileName) {
            var Value = true;
            var buffer = new byte[1024];

            try {
                if (File.Exists(FileName))
                    File.Delete(FileName);
                Stream outStream = File.Create(FileName);
                var inStream = response.GetResponseStream();

                int l;
                do {
                    l = inStream.Read(buffer, 0, buffer.Length);
                    if (l > 0)
                        outStream.Write(buffer, 0, l);
                } while (l > 0);

                outStream.Close();
                inStream.Close();
            }
            catch {
                Value = false;
            }
            return Value;
        }


        /// <summary>
        ///     得到字符串的子字符串。其中一个汉字的长度为2。
        /// </summary>
        /// <param name="temp"></param>
        /// <param name="len"></param>
        /// <returns></returns>
        public static string Substring(string temp, int len) {
            var encoding = Encoding.GetEncoding("GB2312");
            var bs = encoding.GetBytes(temp);
            var AddString = "";
            if (bs.Length < len) {
                len = bs.Length;
            }
            else {
                AddString = "...";
            }
            var NewByte = new byte[len];
            for (var i = 0; i < len; i++) {
                NewByte[i] = bs[i];
            }

            var str = encoding.GetString(NewByte) + AddString;
            return str.Replace("?", "");
        }


        /// <summary>
        ///     获取QQ当前状态（1：在线，0：不在线，-1：不存在）
        /// </summary>
        /// <param name="qq">qq号</param>
        /// <returns></returns>
        /// <summary>
        ///     去除html
        /// </summary>
        /// <param name="html"></param>
        /// <returns></returns>
        public static string wipescript(string html) {
            if ("" + html == "")
                return "";
            var regex1 = new Regex(@"<script[\s\s]+</script *>", RegexOptions.IgnoreCase);
            var regex2 = new Regex(@" href *= *[\s\s]*script *:", RegexOptions.IgnoreCase);
            var regex6 = new Regex(@" src *= *[\s\s]*script *:", RegexOptions.IgnoreCase);
            var regex3 = new Regex(@" on[\s\s]*=", RegexOptions.IgnoreCase);
            var regex4 = new Regex(@"<iframe[\s\s]+</iframe *>", RegexOptions.IgnoreCase);
            var regex5 = new Regex(@"<frameset[\s\s]+</frameset *>", RegexOptions.IgnoreCase);
            html = regex1.Replace(html, ""); //过滤<script></script>标记 
            html = regex2.Replace(html, ""); //过滤href=javascript: (<a>) 属性 
            html = regex3.Replace(html, " _disibledevent="); //过滤其它控件的on...事件 
            html = regex4.Replace(html, ""); //过滤iframe 
            html = regex5.Replace(html, ""); //过滤frameset 
            html = regex6.Replace(html, ""); //过滤frameset 
            html = Regex.Replace(html, @"<script[^>]*?>.*?</script>", "", RegexOptions.IgnoreCase);
            return html;
        }

        #region

        /// 移除HTML标签
        /// <summary>
        ///     移除HTML标签
        /// </summary>
        /// <param name="HTMLStr">HTMLStr</param>
        public static string ParseTags(string HTMLStr) {
            return Regex.Replace(HTMLStr, "<[^>]*>", "");
        }

        #endregion

        /// <summary>
        ///     去除html
        /// </summary>
        /// <param name="strHtml"></param>
        /// <returns></returns>
        public static string ReplaceHTML(object strHtml1) {
            var strHtml = "" + strHtml1;

            //删除脚本
            strHtml = Regex.Replace(strHtml, @"<script[^>]*?>.*?</script>", "", RegexOptions.IgnoreCase);
            //删除HTML
            strHtml = Regex.Replace(strHtml, @"<(.[^>]*)>", "", RegexOptions.IgnoreCase);
            //strHtml = Regex.Replace(strHtml, @"([\r\n])[\s]+", "", RegexOptions.IgnoreCase);
            strHtml = Regex.Replace(strHtml, @"-->", "", RegexOptions.IgnoreCase);
            strHtml = Regex.Replace(strHtml, @"<!--.*", "", RegexOptions.IgnoreCase);
            strHtml = Regex.Replace(strHtml, @"&(quot|#34);", "\"", RegexOptions.IgnoreCase);
            strHtml = Regex.Replace(strHtml, @"&(amp|#38);", "&", RegexOptions.IgnoreCase);
            strHtml = Regex.Replace(strHtml, @"&(lt|#60);", "<", RegexOptions.IgnoreCase);
            strHtml = Regex.Replace(strHtml, @"&(gt|#62);", ">", RegexOptions.IgnoreCase);
            strHtml = Regex.Replace(strHtml, @"&(nbsp|#160);", " ", RegexOptions.IgnoreCase);
            strHtml = Regex.Replace(strHtml, @"&(iexcl|#161);", "\xa1", RegexOptions.IgnoreCase);
            strHtml = Regex.Replace(strHtml, @"&(cent|#162);", "\xa2", RegexOptions.IgnoreCase);
            strHtml = Regex.Replace(strHtml, @"&(pound|#163);", "\xa3", RegexOptions.IgnoreCase);
            strHtml = Regex.Replace(strHtml, @"&(copy|#169);", "\xa9", RegexOptions.IgnoreCase);
            strHtml = Regex.Replace(strHtml, @"&#(\d+);", "", RegexOptions.IgnoreCase);
            strHtml.Replace("<", "");
            strHtml.Replace(">", "");
            //strHtml.Replace("\r\n", "");
            strHtml = HttpContext.Current.Server.HtmlEncode(strHtml).Trim();
            return strHtml;
        }

        /// <summary>
        ///     去除html,截断字符串
        /// </summary>
        /// <param name="strHtml"></param>
        /// <returns></returns>
        public static string ReplaceHTML(object strHtml1, int len) {
            var strHtml = "" + strHtml1;

            //删除脚本
            strHtml = Regex.Replace(strHtml, @"<script[^>]*?>.*?</script>", "", RegexOptions.IgnoreCase);
            //删除HTML
            strHtml = Regex.Replace(strHtml, @"<(.[^>]*)>", "", RegexOptions.IgnoreCase);
            //strHtml = Regex.Replace(strHtml, @"([\r\n])[\s]+", "", RegexOptions.IgnoreCase);
            strHtml = Regex.Replace(strHtml, @"-->", "", RegexOptions.IgnoreCase);
            strHtml = Regex.Replace(strHtml, @"<!--.*", "", RegexOptions.IgnoreCase);
            strHtml = Regex.Replace(strHtml, @"&(quot|#34);", "\"", RegexOptions.IgnoreCase);
            strHtml = Regex.Replace(strHtml, @"&(amp|#38);", "&", RegexOptions.IgnoreCase);
            strHtml = Regex.Replace(strHtml, @"&(lt|#60);", "<", RegexOptions.IgnoreCase);
            strHtml = Regex.Replace(strHtml, @"&(gt|#62);", ">", RegexOptions.IgnoreCase);
            strHtml = Regex.Replace(strHtml, @"&(nbsp|#160);", " ", RegexOptions.IgnoreCase);
            strHtml = Regex.Replace(strHtml, @"&(iexcl|#161);", "\xa1", RegexOptions.IgnoreCase);
            strHtml = Regex.Replace(strHtml, @"&(cent|#162);", "\xa2", RegexOptions.IgnoreCase);
            strHtml = Regex.Replace(strHtml, @"&(pound|#163);", "\xa3", RegexOptions.IgnoreCase);
            strHtml = Regex.Replace(strHtml, @"&(copy|#169);", "\xa9", RegexOptions.IgnoreCase);
            strHtml = Regex.Replace(strHtml, @"&#(\d+);", "", RegexOptions.IgnoreCase);
            strHtml.Replace("<", "");
            strHtml.Replace(">", "");

            var temp = strHtml.Split(new[] {"\r\n"}, StringSplitOptions.RemoveEmptyEntries);
            var sb = new StringBuilder();
            foreach (var s in temp) {
                if (s.Trim().Length == 0)
                    continue;
                sb = sb.Append(Substring(s, len).Trim() + "<br/>");
            }

            return sb.ToString().Trim();
        }

        /// <summary>
        ///     替换html中的特殊字符
        /// </summary>
        /// <param name="theString">需要进行替换的文本。</param>
        /// <returns>替换完的文本。</returns>
        public static string HtmlEncode(string theString) {
            theString = theString.Replace(" ", "&nbsp;");
            theString = theString.Replace(">", ">");
            theString = theString.Replace("<", "<");
            theString = theString.Replace("&", "&amp;");
            theString = theString.Replace("\"", "&quot;");
            theString = theString.Replace("\'", "'");
            theString = theString.Replace("\n", "<br/> ");
            return theString;
        }

        /// <summary>
        ///     恢复html中的特殊字符
        /// </summary>
        /// <param name="theString">需要恢复的文本。</param>
        /// <returns>恢复好的文本。</returns>
        public static string HtmlDiscode(string theString) {
            theString = theString.Replace("&nbsp;", " ");
            theString = theString.Replace(">", ">");
            theString = theString.Replace("<", "<");
            theString = theString.Replace("&amp;", "&");
            theString = theString.Replace("&quot;", "\"");
            theString = theString.Replace("'", "\'");
            theString = theString.Replace("<br/> ", "\n");
            return theString;
        }


        /// <summary>
        ///     过滤不安全字符
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string SqlSafe(string str) {
            if (str == null)
                return "";
            string[] temp = {
                "'", "|", "&", ";", "$", "%", "@", '"'.ToString(), "\'", "\"", "<>", "()", "+", "\r", "\n",
                ",", "\\"
            };
            foreach (var s in temp) {
                str = str.Replace(s, "");
            }
            return str;
        }

        //截取字符串
        public static string GetCut(string str, int count) {
            if (str.Length > count)
                return str.Substring(0, count) + "...";
            return str;
        }

        /// <summary>
        ///     object
        /// </summary>
        /// <param name="strObj"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static string CutStr(object strObj, int count) {
            if (strObj == null || string.IsNullOrEmpty(strObj.ToString())) {
                return "";
            }
            return GetCut(strObj.ToString(), count);
        }
    }
}