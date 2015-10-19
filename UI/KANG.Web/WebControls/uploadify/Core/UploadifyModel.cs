using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KANG.Web.WebControls.uploadify.Core {
    public class UploadifyModel {

        public UploadifyModel() {
            swf = "/WebControls/uploadify/uploadify.swf";
            uploader = "/xk/uprecieve.aspx";
            auto = false;
            buttonClass = "";
            buttonText = "<span class='glyphicon glyphicon-folder-open'></span> &nbsp;&nbsp;选择文件";
            buttonImage = null;
            fileSizeLimit = "10MB";
            fileTypeDesc = "图片文件";
            fileTypeExts = "*.gif; *.jpg; *.png";
            formData = new Dictionary<string, dynamic>() {{"author", "kangwl"}};
            multi = true;
            queueSizeLimit = 10;
            uploadLimit = 10;
            queueID = "upload_queue";
            width = 135;
            height = 35;
        }

        public string method { get { return "post"; } }

        public string swf { get; set; }
        /// <summary>
        /// exp:/xk/uprecieve.aspx
        /// </summary>
        public string uploader { get; set; }
        /// <summary>
        /// 自动上传
        /// </summary>
        public bool auto { get; set; }
        public string buttonClass { get; set; }
        public string buttonText { get; set; }
        public string buttonImage { get; set; }
        /// <summary>
        /// B, KB, MB, or GB.
        /// The default unit is in KB.
        /// You can set this value to 0 for no limit.
        /// </summary>
        public string fileSizeLimit { get; set; }

        /// <summary>
        /// exp:图片文件
        /// </summary>
        public string fileTypeDesc { get; set; }

        /// <summary>
        /// exp:*.gif; *.jpg; *.png
        /// </summary>
        public string fileTypeExts { get; set; }
        /// <summary>
        /// 额外传值post/get
        /// </summary>
        public Dictionary<string, dynamic> formData { get; set; }

        
        public bool multi { get; set; }

        /// <summary>
        /// 每次添加队列的最大数量
        /// 和 uploadLimit 无关
        /// </summary>
        public int queueSizeLimit { get; set; }
        /// <summary>
        /// 上传文件的最大数量
        /// </summary>
        public int uploadLimit { get; set; }
        /// <summary>
        /// 用于显示队列的div ID
        /// </summary>
        public string queueID { get; set; }

        public int width { get; set; }
        public int height { get; set; }
    }
}