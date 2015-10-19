<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="test.aspx.cs" Inherits="KANG.Web.WebControls.Datepicker.test" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="/WebControls/Datepicker/css/bootstrap-datepicker3.min.css" rel="stylesheet" />
    <script src="/WebControls/Datepicker/js/bootstrap-datepicker.js"></script>
    <script src="/WebControls/Datepicker/locales/bootstrap-datepicker.zh-CN.min.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row">
        <div class="col-md-6">
            <input id="default" type="text" class="form-control"/>
            <input id="default1" type="text" value="2012-08-08" class="form-control"/>
            <hr/>
            <div class="input-group date">
                <input type="text" class="form-control"/>
                <span class="input-group-addon">
                    <span class="glyphicon glyphicon-calendar"></span>
                </span>
            </div>
            <hr/>
            <div class="input-daterange input-group" id="datepicker">
                <input type="text" class="input-sm form-control" name="start" />
                <span class="input-group-addon">至</span>
                <input type="text" class="input-sm form-control" name="end" />
            </div>
        </div>
         <div class="col-md-6">
            
        </div>
    </div>
    <script>
        $("#default").val("1990-06-06");
        $('input,.date').datepicker({
            format: "yyyy-mm-dd",
            autoclose: true,
            language: 'zh-CN',
            todayBtn: "linked",
            todayHighlight: true
        });

        $('.input-daterange').datepicker({
            format: "yyyy-mm-dd",
            todayBtn: "linked",
            language: "zh-CN",
            autoclose: true,
            todayHighlight: true
        });
    </script>
</asp:Content>
