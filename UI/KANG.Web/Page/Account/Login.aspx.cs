using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using KANG.Common;
using KANG.Redis;

namespace KANG.Web.Page.Account {
    public partial class Login : System.Web.UI.Page {
  
        private string sessionKey = "test_key";

        protected void Page_Load(object sender, EventArgs e) {
            if (IsPostBack) return;
            //if (Session[sessionKey] != null) {
            //    Log4net.Info(sessionKey);
            //    Response.Write((DateTime)Session[sessionKey]);
            //}
            //else {
            //    Response.Write("session null");
            //    Session.Timeout = 10;
            //    Session[sessionKey] = DateTime.Now;
            //}
             
        }
 
        protected void btn_Show_Click(object sender, EventArgs e) {
   

        }

 

    }
}