<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="KANG.Web.Index" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <select>
        <option>12334</option>
        <option>wer</option>
    </select>
    <script>
        $(function() {
            $("select").click();
        })
    </script>
</asp:Content>
