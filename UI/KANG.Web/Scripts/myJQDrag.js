var myJQDrag = {
    dragObj: null,
    dragerPos: { x: 0, y: 0 },
    canDrag: false,
    mousePos: {
        ori: { x: 0, y: 0 }
    },
    init: function ($obj,handleSelector) {
        myJQDrag.dragObj = $obj;
        //禁用选择文字
        $(document).delegate(myJQDrag.dragObj, "selectstart",
            function () {
                return false;
            }
        );
        //mousedown
        myJQDrag.dragObj.delegate(myJQDrag.dragObj, "mousedown", function (e) {
            myJQDrag.dealMouseDown(e);
        });
        //mousemove
        $(document).delegate(document, "mousemove", function (e) {
            if (myJQDrag.canDrag) {
                myJQDrag.dealMouseMove(e);
            }
        });
        //mouseup
        $(document).delegate(document, "mouseup", function (e) {
            myJQDrag.dealMouseUp(e);
        });
    },
    dealMouseDown: function (e) {
        myJQDrag.canDrag = true;
        //记录down时的鼠标位置
        var downX = e.clientX;
        var downY = e.clientY;
        myJQDrag.mousePos.ori.x = downX;
        myJQDrag.mousePos.ori.y = downY;

        //记录拖动对象的位置
        var position = myJQDrag.dragObj.position();
        myJQDrag.dragerPos.x = position.left;
        myJQDrag.dragerPos.y = position.top;
    },
    dealMouseMove: function (e) {

        var nowX = e.clientX;
        var nowY = e.clientY;
        $("#txt_in").val(nowX + " " + nowY);
        var xx = nowX - myJQDrag.mousePos.ori.x + myJQDrag.dragerPos.x;
        var yy = nowY - myJQDrag.mousePos.ori.y + myJQDrag.dragerPos.y;

        myJQDrag.dragObj.css({ "left": xx, "top": yy });
    },
    dealMouseUp: function (e) {
        myJQDrag.canDrag = false;
    }
}