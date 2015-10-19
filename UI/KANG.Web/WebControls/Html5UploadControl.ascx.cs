using System;
using System.Collections.Generic;
using KANG.Web.WebControls.Html5Upload.Core;
using KANG.Common;

namespace KANG.Web.WebControls {
    public partial class Html5UploadControl : System.Web.UI.UserControl {

        private Html5Upload.Core.UploadModel UploadModel;

        public string UploadJson;

        public Html5UploadControl() {
            UploadModel = new UploadModel();
            Accept = "*/*";
        }

        protected void Page_Load(object sender, EventArgs e) {
            if (string.IsNullOrEmpty(UploadUrl)) {
                throw new ArgumentNullException("UploadUrl", "UploadUrl 不能为空");
            }
            UploadJson = UploadModel.ToJson();
        }

        /// <summary>
        /// 接收上传文件的服务地址
        /// </summary>
        public string UploadUrl
        {
            get { return UploadModel.uploadUrl; }
            set { UploadModel.uploadUrl = value; }
        }

        /// <summary>
        /// 异步上传
        /// </summary>
        public bool UploadAsync
        {
            get { return UploadModel.uploadAsync; }
            set { UploadModel.uploadAsync = value; }
        }

        /// <summary>
        /// 拖拽功能
        /// </summary>
        public bool DropZoneEnabled
        {
            get { return UploadModel.dropZoneEnabled; }
            set { UploadModel.dropZoneEnabled = value; }
        }

        /// <summary>
        /// 上传最大kb，单位KB
        /// </summary>
        public int MaxFileSize
        {
            get { return UploadModel.maxFileSize; }
            set { UploadModel.maxFileSize = value; }
        }

        /// <summary>
        /// 选择文件的提示文本
        /// </summary>
        public string BrowseLabel
        {
            get { return UploadModel.browseLabel; }
            set { UploadModel.browseLabel = value; }
        }

        public string RemoveLabel
        {
            get { return UploadModel.removeLabel; }
            set { UploadModel.removeLabel = value; }
        }

        public string InitialCaption
        {
            get { return UploadModel.initialCaption; }
            set { UploadModel.initialCaption = value; }
        }

        public string UploadLabel
        {
            get { return UploadModel.uploadLabel; }
            set { UploadModel.uploadLabel = value; }
        }


        public string BrowseIcon
        {
            get { return UploadModel.browseIcon; }
            set { UploadModel.browseIcon = value; }
        }

        public List<string> AllowedPreviewTypes
        {
            get { return UploadModel.allowedPreviewTypes; }
            set { UploadModel.allowedPreviewTypes = value; }
        }

        public List<string> AllowedFileExtensions
        {
            get { return UploadModel.allowedFileExtensions; }
            set { UploadModel.allowedFileExtensions = value; }
        }

        //
        public bool MutiEnable { get; set; }

        public string MutiStr
        {
            get
            {
                if (MutiEnable) {
                    return "multiple";
                }
                return "";
            }
        }

        /// <summary>
        /// 显示全部文件类型：*/*
        /// 显示图片类型：image/*
        /// 显示文本类型：text/*
        /// 显示图片和文本类型：image/*,text/*
        /// </summary>
        public string Accept { get; set; }
         
        
    }
}