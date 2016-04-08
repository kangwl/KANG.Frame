﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="CodeRain.aspx.cs" Inherits="KANG.Web.Test.CodeRain.CodeRain" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script>
 /*
     ① 用setInterval(draw, 33)设定刷新间隔
     
     ② 用String.fromCharCode(1e2+Math.random()*33)随机生成字母
     
     ③ 用ctx.fillStyle=’rgba(0,0,0,.05)’; ctx.fillRect(0,0,width,height); ctx.fillStyle=’#0F0′; 反复生成opacity为0.5的半透明黑色背景
     
     ④ 用x = (index * 10)+10;和yPositions[index] = y + 10;顺序确定显示字母的位置
     
     ⑤ 用fillText(text, x, y); 在指定位置显示一个字母 以上步骤循环进行，就会产生《黑客帝国》的片头效果。
 */
     $(document).ready(function() {
         var s = window.screen;
         var width = q.width = s.width;
         var height = q.height;
         var yPositions = Array(300).join(0).split('');
         var ctx = q.getContext('2d');
         var draw = function() {
             ctx.fillStyle = 'rgba(0,0,0,.05)';
             ctx.fillRect(0, 0, width, height);
             ctx.fillStyle = 'red';
             ctx.font = '10pt Consolas';
             yPositions.map(function(y, index) {
                 text = String.fromCharCode(1e2 + Math.random() * 33);
                 x = (index * 10) + 10;
                 q.getContext('2d').fillText(text, x, y);
                 if (y > Math.random() * 1e4) {
                     yPositions[index] = 0;
                 } else {
                     yPositions[index] = y + 10;
                 }
             });
         };
         RunMatrix();
         function RunMatrix() {
             Game_Interval = setInterval(draw,30);
         }
     });
 </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <canvas id="q" class="container" style="z-index: 1;position: absolute " width="500" height="500">
    </canvas>
 
</asp:Content>
