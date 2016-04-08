using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using KANG.Redis;

namespace KANG.Web.Test.Redis {
    public partial class Publish : System.Web.UI.Page {
        protected void Page_Load(object sender, EventArgs e) {

        }

        private KANG.Redis.RedisCommon redisCommon = new RedisCommon();
        protected void btn_Publish_Click(object sender, EventArgs e) {
            redisCommon.Publish("news", txt_Content.Text);
        }
    }
}