using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using KANG.Common;

namespace KANG.Web.WebControls.Test {
    public partial class uploadify : System.Web.UI.Page {
        protected void Page_Load(object sender, EventArgs e) {

            if (IsPostBack) return;
            LoadControl();
        }
        /// <summary>
        /// 是否支持HTML5
        /// </summary>
        public bool SupportHtml5 { get { return Request.QueryString["html5"].ToInt() == 1; } }

        private string uploadRecieveUrl = "/xk/UpRecieve.aspx";

        private void LoadControl() {
            //如果不支持html5就加载flash上传
            string uploadControlUrl = "~/WebControls/FlashUploadify.ascx";
            if (SupportHtml5) {
                uploadControlUrl = "~/WebControls/Html5UploadControl.ascx";
            }
            Control control = Page.LoadControl(uploadControlUrl);
            if (SupportHtml5) {
                Html5UploadControl html5Upload = control as Html5UploadControl;
                if (html5Upload != null) {
                    html5Upload.UploadUrl = uploadRecieveUrl;
                    html5Upload.MutiEnable = true;
                }
            }
            else {
                FlashUploadify flashUploadify = control as FlashUploadify;
                if (flashUploadify != null) flashUploadify.uploader = uploadRecieveUrl;
            }
            UploadControl.Controls.Add(control);
        }
    }
}