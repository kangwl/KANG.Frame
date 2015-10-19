/*
自定义弹出框
*/
var myJQDialog = {
	classes: {
		alertClass: "my_alert_div",
		confirmClass: "my_confirm_div",
        rightBottomAlertClass:"my_right_bottom_div"
	},
	maskId: "my_mask_div",//背景遮罩层 id
    totalMaskDialogs:function() {
        var alertCount = $("." + myJQDialog.classes.alertClass).length;
        var confirmCount = $("." + myJQDialog.classes.confirmClass).length;

        var totalCount = alertCount + confirmCount;
        return totalCount;
    },
	option: {
        //遮罩层
	    mask: { "background-color": "#F8F8FF", "z-index": "9999","-ms-opacity":0.6,"opacity":0.6 },
		alert: {
			alertMainCss: { "z-index": "999999", "background-color": "#ccc", "width": "350px" },
			alertHeaderCss: { "height": "35px", "background-color": "#48d1cc" },
			alertTitleCss: { "font-size": "110%", "color": "#fff","font-weight":"800" },
			alertCloseCss: { "font-size": "20px", "color": "#000" },
			alertContentCss: { "background-color": "#fff","text-align":"left","font-size":"95%","border":"1px solid #ddd" }
		},
        drag:false
	},
	showMask: function () {

	    var mask_div_obj = myJQDialog.getCurrentMask();
	    if (mask_div_obj.length < 1) {
	        mask_div_obj = $("<div></div>");
	        mask_div_obj.attr("id", myJQDialog.maskId);
	        mask_div_obj.css({ "display": "none", "position": "absolute", "left": "0", "top": "0", "width": "100%", "height": "100%" });
	        mask_div_obj.css(myJQDialog.option.mask);
	        mask_div_obj.appendTo("body");
	    }

	    return mask_div_obj;
	},
	getCurrentMask: function () {
	    var maskObj = $("#" + myJQDialog.maskId);
		return maskObj;
	},
	getLastShowObjByClass: function (cls) {
	    var objs = $("." + cls);
	    var leng = objs.length;
	    return objs.eq(leng - 1);
	},
    //autoCenter:function() {
    //    $(window).resize(function () {
    //        myJQDialog.center.alertResize();
    //        myJQDialog.center.confirmResize();
    //    });
    //},
	position: {
	    alertCenter: function () {
	        myJQDialog.position.centerAuto(myJQDialog.classes.alertClass);
		},
	    confirmCenter: function () {
	        myJQDialog.position.centerAuto(myJQDialog.classes.confirmClass);
		},
        centerAuto:function(cls) {
            myJQDialog.setPositon.setCenter(cls);
        },
        rightBottomAuto:function() {
            myJQDialog.setPositon.setRightBottom(myJQDialog.classes.rightBottomAlertClass);
        }
	},
    setPositon: {
        setCenter: function(cls) {
            var screenWidth = 0, screenHeight = 0, scrollTop = 0;
            scrollTop = $(window).scrollTop();
            screenWidth = $(window).width();
            screenHeight = $(window).height();

            var lastObj = myJQDialog.getLastShowObjByClass(cls);
            
            var showDivWidth = lastObj.width();
            var showDicHeight = lastObj.height();

            var left = (screenWidth - showDivWidth) / 2;
            var top = (screenHeight - showDicHeight) / 2 + scrollTop;
            if (parseInt(top, 10) < 0) {
                top = 15;
            }

            lastObj.css("left", left + "px").css("top", top + "px");
        },
        setRightBottom: function (cls) {
            var lastObj = myJQDialog.getLastShowObjByClass(cls);
            lastObj.css("right", "0").css("bottom", "0");
        }
    },
    remove: function (obj) {
        var maskDialogCount = myJQDialog.totalMaskDialogs();
        if (obj.attr("id") == myJQDialog.maskId) {
            if (maskDialogCount == 0) {
                obj.remove();
            }
        } else {
            obj.remove();
        }
    },
    //展现数据到弹出层
    showData: function(showWidth, showTitle,data) {
        myJQDialog.option.alert.alertMainCss.width = showWidth;
        var alertObj = myJQDialog.alert(showTitle, data);
    },
    //右下角弹出框
	rightBottomAlert: function (title, content,width,height,optionObj) {

        if ($.trim(optionObj) != "") {
            myJQDialog.option.alert = optionObj;
        }
	    var widthShow=400, heightShow=250;
        if ($.trim(width) != "") {
            if (width.toString().indexOf("px") != -1) {
                widthShow = width;
            } else {
                widthShow = width + "px";
            }
        }
        if ($.trim(height) != "") {
            if (height.toString().indexOf("px") != -1) {
                heightShow = height;
            } else {
                heightShow = height + "px";
            }
        }

        var alert_div_obj = myJQDialog.createBaseAlert(myJQDialog.classes.rightBottomAlertClass, title, content, "slide");
        alert_div_obj.css({ "width": widthShow, "height": heightShow, "right": "0", "bottom": "0", "overflow": "hidden" });

        var lastObj = myJQDialog.getLastShowObjByClass(myJQDialog.classes.rightBottomAlertClass);
        lastObj.find("#my_alert_div_content").css({ "border": "1px solid #ddd", "text-align": "left", "text-indent": "2em" });

        alert_div_obj.slideDown("slow");
	},
 
    //弹出框基准
	createBaseAlert: function (alertClass, title, content, effect) {

	    if (content != "iframe") {
	        if ($.trim(content) == "") {
	            content = title;
	            title = "提示信息";
	        }
	        if ($.trim(title) == "") {
	            title = "提示信息";
	        }
	    } else {
	        content = "";
	    }

	    var alert_div_obj = $("<div>");
        alert_div_obj.attr("class", alertClass);
        alert_div_obj.css({ "display": "none", "position": "absolute" });
        alert_div_obj.css(myJQDialog.option.alert.alertMainCss);

        var alert_div_header_obj = $("<div>");
        alert_div_header_obj.attr("id", "alert_div_header");
        alert_div_header_obj.css("width", "100%");
        alert_div_header_obj.css(myJQDialog.option.alert.alertHeaderCss);

        var headerHeight = myJQDialog.option.alert.alertHeaderCss.height;

        var alert_div_title_obj = $("<span>");
        alert_div_title_obj.attr("id", "alert_div_title");
        alert_div_title_obj.css({ "position": "absolute", "left": "5px", "display": "block" });
        alert_div_title_obj.css({ "height": headerHeight, "line-height": headerHeight });
        alert_div_title_obj.css(myJQDialog.option.alert.alertTitleCss);
        alert_div_title_obj.html(title);

        var alert_div_close_obj = $("<span>");
        alert_div_close_obj.html("×");
        alert_div_close_obj.attr("title", "点击关闭");
        alert_div_close_obj.attr("id", "alert_div_close");
        alert_div_close_obj.css({ "display": "block", "position": "absolute", "right": "5px", "cursor": "pointer" });
        alert_div_close_obj.css({ "height": headerHeight, "line-height": headerHeight });
        alert_div_close_obj.css(myJQDialog.option.alert.alertCloseCss);
        if (effect == "slide") {
            alert_div_close_obj.click(function () {
                alert_div_obj.slideUp("slow", function () {
                    myJQDialog.remove(alert_div_obj);
                 });
            });
        } else {
            alert_div_close_obj.click(function() {
                alert_div_obj.fadeOut(function() {
                    myJQDialog.remove(alert_div_obj);
                    myJQDialog.remove(myJQDialog.getCurrentMask());
                 });
            });
        }
        alert_div_close_obj.hover(function () {
            $(this).css("color", "red");
        }, function () {
            $(this).css("color", "#000");
        });

        var alert_div_content_obj = $("<div>");
        alert_div_content_obj.attr("id", "my_alert_div_content");
        alert_div_content_obj.css({ "height": "100%", "padding": "5px", "padding-top": "20px", "padding-bottom": "20px" });
        alert_div_content_obj.css(myJQDialog.option.alert.alertContentCss);
        alert_div_content_obj.html(content);

        alert_div_close_obj.appendTo(alert_div_header_obj);
        alert_div_title_obj.appendTo(alert_div_header_obj);

        alert_div_header_obj.appendTo(alert_div_obj);
        alert_div_content_obj.appendTo(alert_div_obj);
        alert_div_obj.appendTo("body");
        if (myJQDialog.option.drag) {
            alert_div_header_obj.hover(function() {
                $(this).css("cursor", "all-scroll");
            }, function() {
                $(this).css("cursor", "default");
            });
	        myJQDrag.init(alert_div_obj);
	    }
	    return alert_div_obj;
    },
    //弹出框
	alert: function (title, content,opt) {
        if (opt) {
            $.extend(myJQDialog.option, opt);
        }
	    var alert_div_obj = myJQDialog.createBaseAlert(myJQDialog.classes.alertClass, title, content);
	    myJQDialog.setPositon.setCenter(alert_div_obj.attr("class"));

		myJQDialog.showMask().fadeIn(function () {
			alert_div_obj.fadeIn();
		});

	    return alert_div_obj;
	},
    //加载iframe弹出框
    alertIframe: function(title, url, width, height) {
	    var $iframe = $("<iframe></iframe>");
	    $iframe.attr({ "src": url, "marginwidth": "0", "marginheight": "0", "frameborder": "0", "scrolling": "auto", "width": width, "height": height });

	    var alert_div_obj = myJQDialog.createBaseAlert(myJQDialog.classes.alertClass, title, "iframe");
        alert_div_obj.css("width", "auto");
	    alert_div_obj.find("#my_alert_div_content").css("text-align", "center").html($iframe);
	    myJQDialog.setPositon.setCenter(alert_div_obj.attr("class"));

	    myJQDialog.showMask().fadeIn(function() {
	        alert_div_obj.fadeIn();
	    });

	},
    //自动消失的弹出框
	alertAutoHide: function (title, content, seconds) {
	    if (arguments.length == 1) {
	        content = arguments[0];
            title = "提示";
            seconds = 3;
        }
        else if (arguments.length==2) {
            seconds = 3;
        }
	    myJQDialog.option.alert.alertContentCss["text-align"] = "center";
        var alertObj = myJQDialog.alert(title, content);
        setTimeout(function() {
            myJQDialog.remove(alertObj);
            myJQDialog.remove(myJQDialog.getCurrentMask());
        }, parseInt(seconds * 1000, 10));
	},
    //自动跳转的弹出框
	alertAutoJump: function (title, content, seconds, url, showSeconds) {
        if (arguments.length == 4) {
            title = "提示";
            content = arguments[0];
            seconds = arguments[1];
            url = arguments[2];
            showSeconds = arguments[3];
        } else if (arguments < 4) {
            document.write("myJQDialog.alertAutoJump 参数不全");
	    }
	    myJQDialog.option.alert.alertContentCss["text-align"] = "center";
        var alertObj;
        if (showSeconds) {
            alertObj = myJQDialog.alert(title, content + " " + seconds);
            setInterval(function() {
                seconds = seconds - 1;
                var contentTxt = content + " " + seconds;
                var total = $("."+myJQDialog.classes.alertClass).length;
                var seldIndex = parseInt(total - 1, 10);
                $("." + myJQDialog.classes.alertClass).eq(seldIndex).find("#my_alert_div_content").html(contentTxt);
            }, 1000);
        } else {
            alertObj = myJQDialog.alert(title, content);
        }
        setTimeout(function () {
            myJQDialog.remove(alertObj);
            myJQDialog.remove(myJQDialog.getCurrentMask());
            window.location.href = url;
        }, parseInt(seconds * 1000, 10));

	},
    //确认框，带回掉函数
	confirm: function (title, content, btn_ok_callback, btn_cancel_callback, opt) {
        if (opt) {
            $.extend(myJQDialog.option, opt);
        }
		if ($.trim(content) == "") {
			content = title;
			title = "提示信息";
		}
		var confirm_title = $.trim(title);
		if (confirm_title == "") {
			confirm_title = "提示信息";
		}
        //confirm div
		var div_confirm_obj = $("<div>").attr("class", myJQDialog.classes.confirmClass);
		div_confirm_obj.css({ "display": "none", "position": "absolute", "z-index": "222", "width": "350px", "left": "348.5px", "top": "72px", "background-color": "rgb(204, 204, 204)" });
		div_confirm_obj.css("border","1px solid #ddd")
        //header start
		var div_confirm_header_obj = $("<div>").attr("id", "div_confirm_header");
		div_confirm_header_obj.css({ "width": "100%", "height": "35px", "background-color": "rgb(72, 209, 204)" });

		var span_confirm_header_close_obj = $("<span>").attr("id", "span_confirm_header_close");
		span_confirm_header_close_obj.css({ "display": "block", "position": "absolute", "right": "5px", "cursor": "pointer", "height": "35px", "line-height": "35px", "font-size": "20px", "color": "rgb(0, 0, 0)" });
		span_confirm_header_close_obj.html("×");
		span_confirm_header_close_obj.attr("title", "点击关闭");
		span_confirm_header_close_obj.click(function () {
			div_confirm_obj.fadeOut(function () {
			    myJQDialog.remove(div_confirm_obj);
			    myJQDialog.remove(myJQDialog.getCurrentMask());
			});
		});
		span_confirm_header_close_obj.hover(function () {
		    $(this).css("color", "red");
		}, function () {
		    $(this).css("color", "#000");
		});

		var span_confirm_header_title_obj = $("<span>").attr("id", "span_confirm_header_title");
		span_confirm_header_title_obj.css({ "position": "absolute", "left": "5px", "display": "block", "height": "35px", "line-height": "35px", "font-size": "110%", "color": "rgb(255, 255, 255)" });
		span_confirm_header_title_obj.html(confirm_title);

		span_confirm_header_title_obj.appendTo(div_confirm_header_obj);
		span_confirm_header_close_obj.appendTo(div_confirm_header_obj);
		div_confirm_header_obj.appendTo(div_confirm_obj);
        //header end
        //content
		var div_confirm_content_obj = $("<div>").attr("id", "div_confirm_content");
		div_confirm_content_obj.css({ "height": "100%", "padding": "20px 5px", "background-color": "rgb(255, 255, 255)" });
		div_confirm_content_obj.html(content);
		div_confirm_content_obj.appendTo(div_confirm_obj);

        //buttons div
		var div_confirm_buttons_obj = $("<div>").attr("id", "div_confirm_buttons");
		div_confirm_buttons_obj.css({ "width": "100%", "text-align": "right", "padding-top": "3px", "padding-bottom": "3px", "background-color": "#FFF", "border-top": "1px solid #CCC" });
        //buttons
		var btn_confirm_buttons_ok_obj = $("<input type=\"button\">").attr("id", "btn_confirm_buttons_ok").val("确定");
		btn_confirm_buttons_ok_obj.css("margin-right", "10px").css({"border":"1px solid #ccc","background-color":"#eee"});
		btn_confirm_buttons_ok_obj.click(function () {
			div_confirm_obj.fadeOut(function () {
			    myJQDialog.remove(div_confirm_obj);
			    myJQDialog.remove(myJQDialog.getCurrentMask());
				btn_ok_callback();
			});
		});
		btn_confirm_buttons_ok_obj.hover(function () {
		    $(this).css("border-color", "#48D1CC");
		}, function () {
		    $(this).css("border-color", "#ccc");
		});


		var a_confirm_buttons_cancel_obj = $("<a>").attr("id", "a_confirm_buttons_cancel").attr("href", "javascript:void(0)").text("取消");
		a_confirm_buttons_cancel_obj.css("margin-right", "10px");
		a_confirm_buttons_cancel_obj.click(function () {
			div_confirm_obj.fadeOut(function () {
			    myJQDialog.remove(div_confirm_obj);
			    myJQDialog.remove(myJQDialog.getCurrentMask());
				btn_cancel_callback();
			});
		});

		btn_confirm_buttons_ok_obj.appendTo(div_confirm_buttons_obj);
		a_confirm_buttons_cancel_obj.appendTo(div_confirm_buttons_obj);
		div_confirm_buttons_obj.appendTo(div_confirm_obj);

		div_confirm_obj.appendTo("body");

		myJQDialog.setPositon.setCenter(div_confirm_obj.attr("class"));
		myJQDialog.showMask().fadeIn(function () {
			div_confirm_obj.fadeIn();
		});

		if (myJQDialog.option.drag) {
		    div_confirm_header_obj.hover(function() {
		        $(this).css("cursor", "all-scroll");
		    },function() {
		        $(this).css("cursor", "default");
		    });

            myJQDrag.init(div_confirm_obj);
        }
	}
}