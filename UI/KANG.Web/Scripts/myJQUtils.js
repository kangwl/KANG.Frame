var myJQUtil= {
    select: {
        //使用jQuery和Ajax填充选择框
        init: function(select_id, url, paramsJson, optName) {
            $.getJSON(url, paramsJson, function(json) {
                var len = json.length;
                var options = '';
                for (var i = 0; i < len; i++) {
                    options += ' ' + json[i][optName] + '';
                }
                $("#" + select_id).html(options);
            });
        },
        //获取选中项的文本
        getSelectedText:function(selector) {
            return $.trim($(selector).find("option:selected").text());
        },
        //获取选中项的值
        getSelectVal: function (selector) {
            return $.trim($(selector).val());
        },
        initSelectByVal: function (selector,val) {
            $(selector).val(val);
        }
    },
    imgErrReplace: function() {
        //自动替换丢失的图片
        $("img").error(function() {
            $(this).unbind("error").attr("src", "missing_image.gif");
        });
    },
    //鼠标悬停效果 页面加载完毕使用
    objFadeShow: function(selector, fadeVal) {
        $(function() {
            $(selector).fadeTo("slow", fadeVal);
            // This sets the opacity of the thumbs to fade down to 60% when the pag loads
            $(selector).hover(function() {
                $(this).fadeTo("slow", 1.0);
                // This should set the opacity to 100% on hover
            }, function() {
                $(this).fadeTo("slow", fadeVal);
                // This should set the opacity back to 60% on mouseout
            });
        });
    },
    //让整个div等其他元素可点击
    clickArea: function(areaSelector, aSelector) {
        $(areaSelector).click(function() {
            window.location = $(this).find(aSelector).attr("href");
            return false;
        });
    },
    //在窗口滚动时自动加载内容 bottomHeight EXP:250
    autoLoadedWhenScroll: function(bottomHeight, getDataFunc) {
        $(window).scroll(function() {
            if ((parseInt($(window).scrollTop(), 10) + parseInt($(window).height(), 10) + parseInt(bottomHeight, 10)) >= $(document).height()) {
                //load data
                getDataFunc();
            }
        });
    },
    //自动滚动到页面中的某区域
    autoScrollTo: function(selector) {
        $('html,body').animate(
            { scrollTop: $(selector).offset().top },
            500
        );
    },
    //禁止右键
    forbidRightClick: function() {
        $(document).delegate(document, "contextmenu", function() { return false; });
    },
    //检查某个页面元素是否存在
    checkObjExist: function(selector) {
        if ($(selector).length) {
            return true;
        }
        return false;
    },
    //限制输入字数
    limitInput: function(selector, max) {
        this.each(function() {
            var $obj = $(selector);
            var type = $obj.tagName.toLowerCase();
            var inputType = $obj.type ? $obj.type.toLowerCase() : null;
            if (type == "input" && inputType == "text" || inputType == "password") {
                //Apply the standard maxLength
                this.maxLength = max;
            } else if (type == "textarea") {
                this.onkeypress = function(e) {
                    var ob = e || event;
                    var keyCode = ob.keyCode;
                    var hasSelection = document.selection ? document.selection.createRange().text.length > 0 : this.selectionStart != this.selectionEnd;
                    return!(this.value.length >= max && (keyCode > 50 || keyCode == 32 || keyCode == 0 || keyCode == 13) && !ob.ctrlKey && !ob.altKey && !hasSelection);
                };
                this.onkeyup = function() {
                    if (this.value.length > max) {
                        this.value = this.value.substring(0, max);
                    }
                };
            }
        });
    },
    //检查某一个元素是否可见
    isVisable: function(element) {
        return $(element).is(':visible');
    },
    //把元素放到浏览器中间
    setObjCenter: function(selector) {
        var screenWidth = 0, screenHeight = 0;
        screenWidth = $(window).width();
        screenHeight = $(window).height();

        var showDivWidth = $(selector).width();
        var showDicHeight = $(selector).height();

        var left = (screenWidth - showDivWidth) / 2;
        var top = (screenHeight - showDicHeight) / 2;

        $(selector).css("left", left + "px").css("top", top + "px");
    },
    //获取坐标
    getMouseXY: function () {
        var XY = { x: 0, y: 0 };
        $(document).mousemove(function(e) {
            XY.x = e.pageX;
            XY.y = e.pageY;
        });
        return XY;
    },
    //检查浏览器cookie是否禁用
    cookieIsOpen: function() {
        var dt = new Date();
        dt.setSeconds(dt.getSeconds() + 60);
        document.cookie = "cookietest=1; expires=" + dt.toUTCString();
        var cookiesEnabled = document.cookie.indexOf("cookietest=") != -1;
        if (!cookiesEnabled) {
            //没有启用cookie
            return false;
        }
        return true;
    },
    //验证数字，整数，浮点数，均可验证
    checkNum: function(oNum) {
        if (!oNum) return false;
        var strP = /^\d+(\.\d+)?$/;
        if (!strP.test(oNum)) return false;
        try {
            if (parseFloat(oNum) != oNum) return false;
        } catch (ex) {
            return false;
        }
        return true;
    },
    //验证正整数
    isNumber: function(oNum) {
        if (!oNum) return false;

        var strP = /^\d+$/; //正整数

        if (!strP.test(oNum)) return false;

        return true;
    }, 
    getObjsVals: function (objs) {
        var vals = [];
        $(objs).each(function(i, n) {
            vals.push(n.value);
        });
        var valStr = vals.join(',');
        return valStr;
    },
    checkbox: {
        //根据 parentSelector批量选中或不选中 childSelector 的checkbox
        initSelAll: function(parentSelector, childSelector) {
            $(document).delegate(parentSelector, "click", function (e) {
                var ckboxes = $(childSelector);
                $(ckboxes).each(function (i, n) {
                    n.checked = e.target.checked;
                });
            });
        },
        cancelSelAll: function (selector) {
            var ckboxes = $(selector);
            $(ckboxes).each(function (i, n) {
                n.checked = false;
            });
        },
        getCheckedBoxObjs:function(selector) {
            var ckboxes = $(selector);
            var ckObjs = [];
            $(ckboxes).each(function (i, n) {
                if (n.checked) {
                    ckObjs.push(n);
                }
            });
            return ckObjs;
        },
        getCheckedBoxObjs1: function (ckboxes) {
            var ckObjs = [];
            $(ckboxes).each(function (i, n) {
                if (n.checked) {
                    ckObjs.push(n);
                }
            });
            return ckObjs;
        },
        getCheckedBoxValArr: function (selector) {
            var ckboxes = $(selector);
            var ckValArr = [];
            $(ckboxes).each(function (i, n) {
                if (n.checked) {
                    ckValArr.push(n.value);
                }
            });
            return ckValArr;
        },
        getCheckedBoxValStr: function (selector) {
            var ckValArr = myJQUtil.checkbox.getCheckedBoxValArr(selector);
            return ckValArr.join(',');
        },
        initCheckBoxByVal:function(selector,val) {
            var ckboxes = $(selector);
            $(ckboxes).each(function (i, n) {
                if (n.value==val) {
                    n.checked = true;
                }
            });
        },
        initCheckBox: function (ckboxs,val) {
            $(ckboxs).each(function (i, n) {
                if (n.value == val) {
                    n.checked = true;
                }
            });
        } ,
        selOneCheckBox:function(selector) {
            $(selector).get(0).checked = true;
        },
        getCheckState: function (selector) {
            return $(selector).get(0).checked;
        }
    },
    radio: {
        //var a = myJQUtil.radio.getCheckedRadioObj("input[type=radio][name=radsex]");
        getCheckedRadioObj:function(selector) {
            var radios = $(selector);
            var checkedRads = [];
            $(radios).each(function(i, n) {
                if (n.checked) {
                    checkedRads.push(n);
                }
            });
            return checkedRads[0];
        },
        // myJQUtil.radio.initOneRadioByVal("input[type=radio][name=radsex]", 2);
        initOneRadioByVal:function(selector,val) {
            var radios = $(selector);
            $(radios).each(function (i, n) {
                if (n.value == val) {
                    n.checked = true;
                }
            });
        }
    },
    //日期显示，需应用jquery ui的datepicker
    datepicker: function (selector, option) {
        if ($.datepicker) {
            if ($.datepicker.regional["zh-CN"] != undefined) {
                $.datepicker.regional["zh-CN"].monthNamesShort = [1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12];
                $.datepicker.regional["zh-CN"].dateFormat = "yy-mm-dd";
                $.datepicker.setDefaults($.datepicker.regional['zh-CN']);
            }
            if ($.trim(option) == "") {
                $(selector).datepicker({
                    showAnim: "slideDown", //effect
                    changeMonth: true,
                    changeYear: true,
                    autoSize: true //自适应文本框
                });
            } else {
                $(selector).datepicker(option);
            }
        }
    },
    cacheScript: function(url, options) {

        // Allow user to set any option except for dataType, cache, and url
        options = $.extend(options || {}, {
            dataType: "script",
            cache: true,
            url: url
        });
        // Use $.ajax() since it is more flexible than $.getScript
        // Return the jqXHR object so we can chain callbacks
        return jQuery.ajax(options);
        // Usage
        //$.cachedScript("ajax/test.js").done(function (script, textStatus) {
        //    console.log(textStatus);
        //});
    },
    getLocationValByKey:function(key) {
        var lSearch = location.search.substr(1);
        var lSearchArr = lSearch.split('&');
        if (lSearchArr.length < 1) {
            lSearchArr = lSearch.split('&amp;');
        }
        var siteVal = 0;
        $(lSearchArr).each(function (i, n) {
            if (n.indexOf(key) != -1) {
                siteVal = $.trim(n.split('=')[1]);
                return false;
            } else {
                return true;
            }
        });
        return siteVal;
    },
    heights: {
        //页面的可视区域（不是内容的高度）
        clientHeight:function() {
            return document.body.clientHeight;
        },
        //网页正文高
        offsetHeight:function() {
            return document.body.offsetHeight;
        },
        //屏幕高度 包括 Windows 任务栏
        screenHeight:function() {
            return window.screen.height;
        },
        //屏幕宽度 包括  Windows 任务栏
        screenWidth:function() {
            return window.screen.width;
        },
        //屏幕可用高度 除 Windows 任务栏之外
        screenAvailHeight:function() {
            return window.screen.availHeight;
        },
        //屏幕可用宽度 除 Windows 任务栏之外
        screenAvailWidth:function() {
            return window.screen.availWidth;
        }
    },
    CutStringLength: function(strString, Length) {
        if (strString != null && strString != "") {
            if (strString.replace(/[^\x00-\xff]/g, "xx").length > Length) {
                var realLength = 0;
                for (var i = 0; i < strString.length; i++) {
                    if (strString.charCodeAt(i) <= 255 && strString.charCodeAt(i) >= 0) {
                        realLength++;
                    } else {
                        realLength += 2;
                    }
                    if (realLength >= Length) {
                        return strString.substring(0, i) + "...";
                    }
                }
            } else {
                return strString;
            }
        } else {
            return "";
        }
    },
    isHtml5:function() {
        return window.applicationCache;
    }
}

// 对Date的扩展，将 Date 转化为指定格式的String   
// 月(M)、日(d)、小时(h)、分(m)、秒(s)、季度(q) 可以用 1-2 个占位符，   
// 年(y)可以用 1-4 个占位符，毫秒(S)只能用 1 个占位符(是 1-3 位的数字)   
// 例子：   
// (new Date()).Format("yyyy-MM-dd hh:mm:ss.S") ==> 2006-07-02 08:09:04.423   
// (new Date()).Format("yyyy-M-d h:m:s.S")      ==> 2006-7-2 8:9:4.18   
Date.prototype.Format = function (fmt) {
    var o = {
        "M+": this.getMonth() + 1,                 //月份   
        "d+": this.getDate(),                    //日   
        "h+": this.getHours(),                   //小时   
        "m+": this.getMinutes(),                 //分   
        "s+": this.getSeconds(),                 //秒   
        "q+": Math.floor((this.getMonth() + 3) / 3), //季度   
        "S": this.getMilliseconds()             //毫秒   
    };
    if (/(y+)/.test(fmt))
        fmt = fmt.replace(RegExp.$1, (this.getFullYear() + "").substr(4 - RegExp.$1.length));
    for (var k in o)
        if (new RegExp("(" + k + ")").test(fmt))
            fmt = fmt.replace(RegExp.$1, (RegExp.$1.length == 1) ? (o[k]) : (("00" + o[k]).substr(("" + o[k]).length)));
    return fmt;
}



 

 



 
