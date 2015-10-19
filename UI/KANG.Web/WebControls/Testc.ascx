<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Testc.ascx.cs" Inherits="KANG.Web.WebControls.Testc" %>
 <form runat="server">
     

    <asp:Repeater runat="server" ID="rptList">
        <ItemTemplate>
            <li class="text-danger"><%#Eval("ID") %></li>
        </ItemTemplate>
    </asp:Repeater> 
 
 <asp:GridView runat="server" ID="grdTemp" CssClass="table"></asp:GridView>
  </form>