<%@ WebHandler Language="C#" Class="UEditorHandler" %>

using System;
using System.Web;
using System.IO;
using System.Collections;
using MyControls.ueditor.net.App_Code;
using Newtonsoft.Json;
using KANG.Web.WebControls.ueditor.net.App_Code;

public class UEditorHandler : IHttpHandler
{
    public void ProcessRequest(HttpContext context)
    {
        Handler action = null;
        switch (context.Request["action"])
        {
            case "config":
                action = new ConfigHandler(context);
                break;
            case "uploadimage":
                action = new UploadHandler(context, new UploadConfig()
                {
                    AllowExtensions = UEConfig.GetStringList("imageAllowFiles"),
                    PathFormat = UEConfig.GetString("imagePathFormat"),
                    SizeLimit = UEConfig.GetInt("imageMaxSize"),
                    UploadFieldName = UEConfig.GetString("imageFieldName")
                });
                break;
            case "uploadscrawl":
                action = new UploadHandler(context, new UploadConfig()
                {
                    AllowExtensions = new string[] { ".png" },
                    PathFormat = UEConfig.GetString("scrawlPathFormat"),
                    SizeLimit = UEConfig.GetInt("scrawlMaxSize"),
                    UploadFieldName = UEConfig.GetString("scrawlFieldName"),
                    Base64 = true,
                    Base64Filename = "scrawl.png"
                });
                break;
            case "uploadvideo":
                action = new UploadHandler(context, new UploadConfig()
                {
                    AllowExtensions = UEConfig.GetStringList("videoAllowFiles"),
                    PathFormat = UEConfig.GetString("videoPathFormat"),
                    SizeLimit = UEConfig.GetInt("videoMaxSize"),
                    UploadFieldName = UEConfig.GetString("videoFieldName")
                });
                break;
            case "uploadfile":
                action = new UploadHandler(context, new UploadConfig()
                {
                    AllowExtensions = UEConfig.GetStringList("fileAllowFiles"),
                    PathFormat = UEConfig.GetString("filePathFormat"),
                    SizeLimit = UEConfig.GetInt("fileMaxSize"),
                    UploadFieldName = UEConfig.GetString("fileFieldName")
                });
                break;
            case "listimage":
                action = new ListFileManager(context, UEConfig.GetString("imageManagerListPath"), UEConfig.GetStringList("imageManagerAllowFiles"));
                break;
            case "listfile":
                action = new ListFileManager(context, UEConfig.GetString("fileManagerListPath"), UEConfig.GetStringList("fileManagerAllowFiles"));
                break;
            case "catchimage":
                action = new CrawlerHandler(context);
                break;
            default:
                action = new NotSupportedHandler(context);
                break;
        }
        action.Process();
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }
}