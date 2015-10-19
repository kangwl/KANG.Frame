<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="ueeditor.aspx.cs" Inherits="KANG.Web.WebControls.Test.ueeditor" %>

<%@ Register Src="~/WebControls/UEditor.ascx" TagPrefix="uc1" TagName="UEditor" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
  <uc1:UEditor runat="server" id="UEditor" />
</asp:Content>
