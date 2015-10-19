/*给予bootstrap的消息提示条*/
$.extend({
    bsAlertType: { success: "success", info: "info", warning: "warning", danger: "danger" },
    bsAlertBase:function(alertType,text) {
        var $alertdiv = $("<div></div>").addClass("alert alert-dismissable").html(text);
        var $closeBtn = $("<button>&times;</button>")
            .attr({ "type": "button", "class": "close", "data-dismiss": "alert", "aria-hidden": "true" });
        $alertdiv.addClass("alert-" + $.trim(alertType));
        $alertdiv.css("text-align", "center");
        $alertdiv.append($closeBtn);
        return $alertdiv;
    },
    bsAlert:function(bsAlertType,text,seconds) {
        var $alertdiv = this.bsAlertBase(bsAlertType, text);
      
        this.setAlert($alertdiv);
        $("body").prepend($alertdiv);
        if (seconds>0) {
            setTimeout(function () {
                $alertdiv.fadeOut();
            }, seconds * 1000);
        }
    },
    bsAlertSuccess: function (text, showPosition, seconds,jumpHref) {
        var $alertdiv = this.bsAlertBase("success", text);
        if ($.trim(showPosition) == "" || (!showPosition)) {
            
            $("body").prepend($alertdiv);
        } else {
            $(showPosition).prepend($alertdiv);
        }
        if (seconds > 0) {
            setTimeout(function () {
                $alertdiv.fadeOut();
                if ($.trim(jumpHref) != "") {
                    location.href = jumpHref;
                }
            }, seconds * 1000);
        }
    },
    bsAlertInfo: function (text, showPosition, seconds, jumpHref) {
        var $alertdiv = this.bsAlertBase("info", text);
        if ($.trim(showPosition) == "" || (!showPosition)) {
     
            $("body").prepend($alertdiv);
        } else {
            $(showPosition).prepend($alertdiv);
        }
        if (seconds > 0) {
            setTimeout(function () {
                $alertdiv.fadeOut();
                if ($.trim(jumpHref) != "") {
                    location.href = jumpHref;
                }
            }, seconds * 1000);
        }
    },
    bsAlertWarn: function (text, showPosition, seconds, jumpHref) {
        var $alertdiv = this.bsAlertBase("warning", text);
        if ($.trim(showPosition) == "" || (!showPosition)) {
      
            $("body").prepend($alertdiv);
        } else {
            $(showPosition).prepend($alertdiv);
        }
        if (seconds > 0) {
            setTimeout(function () {
                $alertdiv.fadeOut();
                if ($.trim(jumpHref) != "") {
                    location.href = jumpHref;
                }
            }, seconds * 1000);
        }
    },
    bsAlertDanger: function (text, showPosition, seconds, jumpHref) {
        var $alertdiv = this.bsAlertBase("danger", text);
        if ($.trim(showPosition) == "" || (!showPosition)) {
      
            $("body").prepend($alertdiv);
        } else {
            $(showPosition).prepend($alertdiv);
        }
        if (seconds > 0) {
            setTimeout(function () {
                $alertdiv.fadeOut();
                if ($.trim(jumpHref) != "") {
                    location.href = jumpHref;
                }
            }, seconds * 1000);
        }
    },
    bsAlertError: function (text, showPosition, seconds, jumpHref) {
        this.bsAlertDanger(text, showPosition, seconds, jumpHref);
    }
})

 