/*
 说明：拖拽替换插件
      用于拖拽、替换页面中的两个相同类型的元素
      使用需要引用jquery
 使用：$(function () {
            replaceElementUtil.init({ mainSelector: "#mydiv", itemsSelector: "#mydiv .divitem" });
       });
 */
var replaceElementUtil = {
    //参数
    settings: {
        canDrag: false,
        oriCell: null,
        tempElementSelector: "#element_temp_div",//默认
        mainSelector: "#tableid",//要移动替换子项的父id选择器
        itemsSelector: "#tableid td",//子项选择器
        oriItemBackgroundColor: "",//子项的原背景色，无需设置，自动获取
        replacingItemBackgrounColor: "#ffc0cb",//将要被替换时的子项背景色
        tempDivBackgroundColor: "#f5deb3",//创建移动的div的背景色
        mouseDownCellBackgroundColor: "#f5deb3",//默认和tempDivBackgroundColor背景色保持一致
        downCursorStyle: "all-scroll",
        upCursorStyle: "default"
    },
    //入口
    //opt 修改参数
    init: function (opt) {
        if (opt) {
            $.extend(replaceElementUtil.settings, opt);
        }
        //禁用选择文字
        //$(document).delegate(replaceElementUtil.settings.mainSelector + "," + replaceElementUtil.settings.tempElementSelector, "selectstart",
        //    function() {
        //        return false;
        //    }
        //);
        //mousedown
        $(document).delegate(replaceElementUtil.settings.mainSelector, "mousedown", function (e) {
            if (!replaceElementUtil.settings.canDrag) {
                if (!replaceElementUtil.fns.checkCell(e.target)) {
                    return false;
                }
                //处理鼠标按下事件
                replaceElementUtil.dealMouseDown(e);
            }
        });
        //mousemove
        $(document).delegate(document, "mousemove", function (e) {
            if (replaceElementUtil.settings.canDrag) {
                //处理鼠标移动事件
                replaceElementUtil.startMove(e);
            }
        });
        //mouseup
        $(document).delegate(document, "mouseup", function (e) {
            if (!replaceElementUtil.fns.checkCell(replaceElementUtil.settings.oriCell)) {
                return false;
            }
            //处理鼠标弹起事件
            replaceElementUtil.dealMouseUp(e);
        });
    },
    recordParam: {
        ori_cell_pos: { x: 0, y: 0 },//开始mousedown时的单元对象坐标
        ori_mouse_pos: { x: 0, y: 0 }//开始mousedown时的鼠标坐标
    },
    dealMouseDown: function (e) {
        var $td = $(e.target);
        var eHtml = $.trim($td.html());
        if (eHtml == "") {
            return false;
        }

        //record mousedown下的元素
        replaceElementUtil.settings.oriCell = e.target;
        $(e.target).css("cursor", replaceElementUtil.settings.downCursorStyle);
        //记录鼠标down时的鼠标位置
        replaceElementUtil.recordParam.ori_mouse_pos.x = event.clientX || e.clientX;
        replaceElementUtil.recordParam.ori_mouse_pos.y = event.clientY || e.clientY;

        
        //获取单元项的背景色，用于后面还原
        replaceElementUtil.settings.oriItemBackgroundColor = $td.css("background-color");
        //设置该项的背景色，用于突出显示
        $td.css("background-color", replaceElementUtil.settings.mouseDownCellBackgroundColor);
        var itemLineHeight = $td.css("line-height");
  
        // var position = $td.position();
        var position = $td.offset();
        replaceElementUtil.recordParam.ori_cell_pos.x = position.left;
        replaceElementUtil.recordParam.ori_cell_pos.y = position.top;
        var $tempDiv = $(replaceElementUtil.settings.tempElementSelector);
        if ($tempDiv.length < 1) {
            $tempDiv = replaceElementUtil.fns.createTempDiv($tempDiv);
        }

        var theWidth = $td.outerWidth();
        var theHeight = $td.outerHeight();
        $tempDiv.html(eHtml);
        $tempDiv.css("width", theWidth)
            .css("height", theHeight)
            .css("top", position.top)
            .css("left", position.left)
            .css("line-height", itemLineHeight);
        $tempDiv.appendTo("body");
        $tempDiv.hide();
        //设置拖拽状态
        replaceElementUtil.settings.canDrag = true;
    },
    startMove:function(e) {
        var c_X = e.clientX, c_Y = e.clientY;
        //$("#xy").text(e.clientX + "-" + e.clientY);//鼠标相对文档的位置
        var $tempDiv = $(replaceElementUtil.settings.tempElementSelector);
        var xx = c_X - replaceElementUtil.recordParam.ori_mouse_pos.x + replaceElementUtil.recordParam.ori_cell_pos.x;
        var yy = c_Y - replaceElementUtil.recordParam.ori_mouse_pos.y + replaceElementUtil.recordParam.ori_cell_pos.y;

        $tempDiv.css("top", yy);
        $tempDiv.css("left", xx);
        $tempDiv.show();

        //增加体验，被替换项改变背景色
        var tds = $(replaceElementUtil.settings.itemsSelector);
        $(tds).each(function (i, n) {
            var pos = $(n).offset();
            var posX = pos.left, posY = pos.top;

            if (c_X > posX && c_X < posX + n.offsetWidth && c_Y > posY && c_Y < posY + n.offsetHeight) {
                $(n).css("background-color", replaceElementUtil.settings.replacingItemBackgrounColor);
            } else {
                $(n).css("background-color", replaceElementUtil.settings.oriItemBackgroundColor);
                $(replaceElementUtil.settings.oriCell).css("background-color", replaceElementUtil.settings.mouseDownCellBackgroundColor);
            }
        });
    },
    dealMouseUp:function(e) {
        var c_X = e.clientX;
        var c_Y = e.clientY;
        var $tempDiv = $(replaceElementUtil.settings.tempElementSelector);
        if ($.trim($tempDiv.html()) == "") {
            return false;
        }
        var tds = $(replaceElementUtil.settings.itemsSelector);
        $(tds).each(function (i, n) {
            var pos = $(n).offset();
            var posX = pos.left, posY = pos.top;

            if (c_X > posX && c_X < posX + n.offsetWidth && c_Y > posY && c_Y < posY + n.offsetHeight) {
                //交换文本
                replaceElementUtil.settings.oriCell.innerHTML = n.innerHTML;
                n.innerHTML = $tempDiv.html();
                //恢复单元项背景色
                $(n).css("background-color", replaceElementUtil.settings.oriItemBackgroundColor);
            }
        });
        replaceElementUtil.settings.canDrag = false;
        replaceElementUtil.fns.clearMouseDownCellStyle($(replaceElementUtil.settings.oriCell));
        replaceElementUtil.fns.clearTempDiv();
    },
    fns: {
        checkCell: function (obj) {
            //比对正确的replaceElementUtil.settings.itemsSelector下的tagname
            var rightTagName = $(replaceElementUtil.settings.itemsSelector).get(0).tagName.toUpperCase();
            
            if (obj == undefined || obj.tagName.toUpperCase() != rightTagName) {
                return false;
            }
            return true;
        },
        clearMouseDownCellStyle: function ($td) {//还原到原来的背景色
            $td.css("background-color", replaceElementUtil.settings.oriItemBackgroundColor);
            $(replaceElementUtil.settings.itemsSelector).css("cursor", replaceElementUtil.settings.upCursorStyle);
        },
        createTempDiv: function ($tpdiv) {
            $tpdiv = $("<div></div>").attr("id", $.trim(replaceElementUtil.settings.tempElementSelector).substr(1));
            $tpdiv.css("position", "absolute")
                  .css("background-color", replaceElementUtil.settings.tempDivBackgroundColor)//背景颜色
                  .css("cursor", "all-scroll")//鼠标样式
                  .css("opacity", "0.9");//透明度
            return $tpdiv;
        },
        clearTempDiv:function() {
            var $tempDiv = $(replaceElementUtil.settings.tempElementSelector);
            $tempDiv.html("");
            $tempDiv.hide();
        }
    }
}