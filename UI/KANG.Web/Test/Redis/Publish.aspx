<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="Publish.aspx.cs" Inherits="KANG.Web.Test.Redis.Publish" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="panel panel-default">
        <div class="panel-heading">
            <div class="panel-title">
                <p>
                    redis 发布
                </p>
            </div>
        </div>
        <div class="panel-body">
            <table class="table">
                <tr>
                    <td class="text-right">内容</td>
                    <td>
                        <asp:TextBox ID="txt_Content" runat="server" TextMode="MultiLine" CssClass="form-control" style="height: 100px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td></td>
                    <td>
                        <asp:Button runat="server" ID="btn_Publish" Text="发布" CssClass="btn btn-primary btn-block" OnClick="btn_Publish_Click"/>
                    </td>
                </tr>
            </table>
        </div>
    </div>
</asp:Content>
