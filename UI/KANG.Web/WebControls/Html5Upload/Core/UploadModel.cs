using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KANG.Web.WebControls.Html5Upload.Core {
    public class UploadModel {
        public UploadModel() {
            showCaption = false;
            language = "zh";
            browseClass = "btn btn-success";
            browseLabel = "选择图片";
            browseIcon = "<i class=\"glyphicon glyphicon-picture\"></i>&nbsp;";
            allowedPreviewTypes = new List<string> {"image"};
            showUpload = true;
            allowedFileExtensions =new List<string> { "jpg", "gif", "png", "doc","docx","pdf" };
            initialCaption = "请选择文件";
            removeClass = "btn btn-danger";
            removeLabel = "全部移除";
            removeIcon = "<i class=\"glyphicon glyphicon-trash\"></i>&nbsp;";
            uploadClass = "btn btn-info";
            uploadLabel = "上传";
            uploadIcon = "<i class=\"glyphicon glyphicon-upload\"></i>&nbsp;";
            minImageWidth = 50;
            minImageHeight = 50;
            maxImageWidth = null;
            maxImageHeight = null;
            maxFileCount = 6;
            validateInitialCount = true;
            uploadUrl = "";
            uploadAsync = true;
            maxFileSize = 0;
            dropZoneEnabled = true;
        }

        public bool showCaption { get; set; }
        public string language { get; set; }
        public string browseClass { get; set; }
        public string browseLabel { get; set; }
        public string browseIcon { get; set; }
        /// <summary>
        /// 预览类型
        /// </summary>
        public List<string> allowedPreviewTypes { get; set; }
        public bool showUpload { get; set; }
        public List<string> allowedFileExtensions { get; set; }
       // public object previewSettings { get; set; }
        public string initialCaption { get; set; }
        public string removeClass { get; set; }
        public string removeLabel { get; set; }
        public string removeIcon { get; set; }
        public string uploadClass { get; set; }
        public string uploadLabel { get; set; }
        public string uploadIcon { get; set; }
        public int minImageWidth { get; set; }
        public int minImageHeight { get; set; }
        public string maxImageWidth { get; set; }
        public string maxImageHeight { get; set; }
        public int maxFileCount { get; set; }
        public bool validateInitialCount { get; set; }
        /// <summary>
        /// 接收文件的服务器地址
        /// </summary>
        public string uploadUrl { get; set; }
        /// <summary>
        /// 是否异步上传
        /// </summary>
        public bool uploadAsync { get; set; }
        /// <summary>
        /// 是否启用拖拽上传
        /// </summary>
        public bool dropZoneEnabled { get; set; }
        /// <summary>
        /// 0 不限制 单位：kb
        /// </summary>
        public int maxFileSize { get; set; }

        // with plugin options
        //$("#upload_id").fileinput({
        //    showCaption: false,//是否显示input file 框
        //    language: "zh",
        //    browseClass: "btn btn-success",
        //    browseLabel: "选择图片",
        //    browseIcon: "<i class=\"glyphicon glyphicon-picture\"></i>",
        //    previewFileType: "image",
        //    showUpload: true,
        //    allowedFileExtensions: ['jpg', 'gif', 'png', 'doc', 'docx'],
        //    previewSettings: {
        //        // image: { width: "200px", height: "160px" }
        //    },
        //    //allowedFileTypes: ["image"], //['image', 'html', 'text', 'video', 'audio', 'flash', 'object']
        //    //'previewFileType': 'any',
        //    initialCaption: "请选择文件",
        //    removeClass: "btn btn-danger",
        //    removeLabel: "删除",
        //    removeIcon: "<i class=\"glyphicon glyphicon-trash\"></i> ",
        //    uploadClass: "btn btn-info",
        //    uploadLabel: "上传",
        //    uploadIcon: "<i class=\"glyphicon glyphicon-upload\"></i> ",
        //    minImageWidth: 50,
        //    minImageHeight: 50,
        //    //maxImageWidth: 2500,
        //    //maxImageHeight: 2500,
        //    maxFileCount: 4,
        //    validateInitialCount: true,
        //    uploadUrl: "uprecieve.aspx",
        //    uploadAsync: true,
        //    dropZoneEnabled: true,
        //    maxFileSize: 0 //不限制大小，单位：kb
        //});

    }
}