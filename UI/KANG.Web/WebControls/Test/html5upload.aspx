<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="html5upload.aspx.cs" Inherits="KANG.Web.WebControls.Test.html5upload" %>

<%@ Register Src="~/WebControls/Html5UploadControl.ascx" TagPrefix="uc1" TagName="Html5UploadControl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:Html5UploadControl UploadUrl="/xk/uprecieve.aspx" MutiEnable="True" Accept="image/*" runat="server" id="Html5UploadControl" />
</asp:Content>
