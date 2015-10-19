using System;
using System.Web;
using KANG.Common;

namespace KANG.Web.WebControls
{
    public partial class UEditor : System.Web.UI.UserControl
    {
        public UEditor() {
            ClearEditorWhenPost = false;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) {
                editor.Text = ClearEditorWhenPost ? "" : Text;
            }
        }
        /// <summary>
        /// 提交表单后清除编辑器内容 默认不清除
        /// </summary>
        public bool ClearEditorWhenPost { get; set; }

        /// <summary>
        /// 编辑器内容
        /// </summary>
        public string Text {
            get { return HttpUtility.UrlDecode(editor_Content.Text); }
            set { editor.Text = value; }
        }

        /// <summary>
        /// 编辑器高度
        /// </summary>
        public int Height { get { return editor.Height.Value.ToInt(); } set { editor.Height = new System.Web.UI.WebControls.Unit(value); } }

        /// <summary>
        /// 编辑器高度是否自动变化
        /// </summary>
        public bool AutoHeightEnabled { get; set; }

    }
}