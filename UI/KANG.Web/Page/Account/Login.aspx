<%@ Page Title="" Language="C#" Async="true" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="KANG.Web.Page.Account.Login" %>
 <asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="panel panel-default">
        <div class="panel-heading">
            <div class="panel-title">
                <p>登录</p>
            </div>
        </div>
        <div class="panel-body">
            <asp:TextBox runat="server" ID="txt_Content" style="height: 100px" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
        </div>
        <div class="panel-footer">
            <asp:Button runat="server" ID="btn_Show" CssClass="btn btn-primary" Text="显示" OnClick="btn_Show_Click"/>
        </div>
    </div>

</asp:Content>
