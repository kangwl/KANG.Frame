using System.Web;
using MyControls.ueditor.net.App_Code;

namespace KANG.Web.WebControls.ueditor.net.App_Code {
    /// <summary>
    /// Config 的摘要说明
    /// </summary>
    public class ConfigHandler : Handler
    {
        public ConfigHandler(HttpContext context) : base(context) { }

        public override void Process()
        {
            WriteJson(UEConfig.Items);
        }
    }
}