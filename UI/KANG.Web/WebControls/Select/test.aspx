<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="test.aspx.cs" Inherits="KANG.Web.WebControls.Select.test" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="/WebControls/Select/css/bootstrap-select.css" rel="stylesheet" />
    <script src="/WebControls/Select/js/bootstrap-select.js"></script>
    <script src="/WebControls/Select/js/i18n/defaults-zh_CN.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <select class="selectpicker">
        <option value="1">Mustard</option>
        <option value="2">Ketchup</option>
        <option value="3">Relish</option>
      </select>
    <asp:DropDownList runat="server" ID="ddl_Users" CssClass="selectpicker">
        <Items>
            <asp:ListItem Text="kangwl" Value="1"></asp:ListItem>
            <asp:ListItem Text="kangwl1" Value="2"></asp:ListItem>
            <asp:ListItem Selected="True" Text="kangwl2" Value="3"></asp:ListItem>
            <asp:ListItem Text="kangwl3" Value="4"></asp:ListItem>
        </Items>
    </asp:DropDownList>
    <script>
        $('.selectpicker').selectpicker({
            style: 'btn-info',
            liveSearch:true
        });
        //赋值
        $(".selectpicker").selectpicker("val", "3");
        //取值
        $(document).on("change", "select", function (e) {
            bootbox.alert($(e.target).val());
        });
        //$('.selectpicker').selectpicker('hide');
        //$('.selectpicker').selectpicker('show');
 
    </script>
</asp:Content>
