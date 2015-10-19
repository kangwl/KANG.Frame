<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UEditor.ascx.cs" Inherits="KANG.Web.WebControls.UEditor" %>
<script type="text/javascript" charset="utf-8" src="<%=ResolveUrl("/webcontrols/ueditor/ueditor.config.js") %>"></script>
<script type="text/javascript" charset="utf-8" src="<%=ResolveUrl("/webcontrols/ueditor/ueditor.all.min.js") %>"> </script>
<!--建议手动加在语言，避免在ie下有时因为加载语言失败导致编辑器加载失败-->
<!--这里加载的语言文件会覆盖你在配置项目里添加的语言类型，比如你在配置项目里配置的是英文，这里加载的中文，那最后就是中文-->
<script type="text/javascript" charset="utf-8" src="<%=ResolveUrl("/webcontrols/ueditor/lang/zh-cn/zh-cn.js") %>"></script>
<asp:TextBox runat="server" ID="editor" Text="" ClientIDMode="Static" TextMode="MultiLine" Width="100%" Height="350px"></asp:TextBox>
<div style="display: none">
    <asp:TextBox runat="server" ID="editor_Content" ClientIDMode="Static"></asp:TextBox>
</div>
<script type="text/javascript">
    //编辑器额外配置
    var ueOption = {
        //显示选择的功能菜单
        toolbars: [
                [
                'fullscreen', 'source', '|',
                'undo', 'redo', '|',
                'bold', 'italic', 'underline', 'fontborder', 'strikethrough', 'removeformat', 'formatmatch', 'autotypeset', 'pasteplain', '|',
                'forecolor', 'backcolor', 'insertorderedlist', 'insertunorderedlist', 'selectall', 'cleardoc', '|',
                'customstyle', 'paragraph', 'fontfamily', 'fontsize', '|'],
                [
                'justifyleft', 'justifycenter', 'justifyright', 'justifyjustify', '|',
                'link', 'unlink', 'anchor', '|',
                'simpleupload', 'emotion', 'template', 'background', '|',
                'horizontal', 'date', 'time', 'spechars', '|',
                'print', 'preview', 'searchreplace'
                ]
        ],
        maximumWords: 50000, //最大输入文字长度，其他内容不计
        autoHeightEnabled: <%:AutoHeightEnabled.ToString().ToLower() %>,
        initialFrameHeight: <%:Height %>
    };
    //指定编辑器
    var ue = UE.getEditor('editor', ueOption);

    //获取编辑器内容
    function getEditorContent() {
        var editorContent = ue.getContent();
        //编码
        var editorContent_encode = encodeURIComponent(editorContent);
        document.getElementById("editor_Content").value = editorContent_encode;
    }
    //初始化编辑器内容
    function initEditorContent() {
        ue.setContent(decodeURIComponent(document.getElementById("editor").value));
    }
    //提交表单时触发
    $(document).submit(function () {
        //为了防止提交时出现有危险值的提示，
        //先编码赋值，再清空编辑器中的内容
        getEditorContent();
        ue.setContent('');
    });
    //准备完毕
    ue.ready(function () {
        initEditorContent();
    });

</script>