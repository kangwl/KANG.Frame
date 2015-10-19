#region

using System;
using System.Web.Security;

#endregion

namespace KANG.Common {
    public class ShortUrl {
        //可以自定义生成MD5加密字符传前的混合KEY
        private const string key = "weipingmu";
        //要使用生成URL的字符
        private static readonly string[] chars = {
            "a", "b", "c", "d", "e", "f", "g", "h",
            "i", "j", "k", "l", "m", "n", "o", "p",
            "q", "r", "s", "t", "u", "v", "w", "x",
            "y", "z", "0", "1", "2", "3", "4", "5",
            "6", "7", "8", "9", "A", "B", "C", "D",
            "E", "F", "G", "H", "I", "J", "K", "L",
            "M", "N", "O", "P", "Q", "R", "S", "T",
            "U", "V", "W", "X", "Y", "Z"
        };

        public static string[] CreateShortUrl(string url, int codeLen = 6) {
            //对传入网址进行MD5加密
            var hex = FormsAuthentication.HashPasswordForStoringInConfigFile(key + url, "md5");

            var resUrl = new string[1];

            for (var i = 0; i < 1; i++) {
                //把加密字符按照8位一组16进制与0x3FFFFFFF进行位与运算
                if (hex != null) {
                    var hexint = 0x3FFFFFFF & Convert.ToInt32("0x" + hex.Substring(i*8, 8), 16);
                    var outChars = string.Empty;
                    for (var j = 0; j < codeLen; j++) {
                        //把得到的值与0x0000003D进行位与运算，取得字符数组chars索引
                        var index = 0x0000003D & hexint;
                        //把取得的字符相加
                        outChars += chars[index];
                        //每次循环按位右移5位
                        hexint = hexint >> 5;
                    }
                    //把字符串存入对应索引的输出数组
                    resUrl[i] = outChars;
                }
            }
            return resUrl;
        }
    }
}