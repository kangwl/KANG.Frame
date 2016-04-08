using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using KANG.Common.web;

namespace ConsoleApp.Test {
    public class MyWebReq {
        public string GetWebHtml(string url) {
            KANG.Common.web.HttpWebHelper helper = new HttpWebHelper(url);
            

            return helper.GetResponseStr();
        }
    }
}
