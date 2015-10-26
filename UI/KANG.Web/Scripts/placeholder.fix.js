//兼容低版本IE的 placeholder
//$("input").myPlaceholder();
(function ($) {
    $.fn.myPlaceholder = function (options) {
        var defaults = {
            pColor: "#acacac",
            pFont: "16px",
            posL: 8,
            zIndex: "99"
        },
        opts = $.extend(true, defaults, options);
 
        if ("placeholder" in document.createElement("input")) return;
        return this.each(function () {
            //$(this).parent().css("position", "relative");
            var isIE = $.browser.msie,
            version = $.browser.version;
 
            //不支持placeholder的浏览器
            var $this = $(this),
                msg = $this.attr("placeholder"),
                iH = $this.outerHeight(),
                iW = $this.outerWidth(),
                iX = $this.offset().left,
                iY = $this.offset().top,
                oInput = $("<label>", {
                    "text": msg,
                    "css": {
                        "position": "absolute",
                        "left": iX + "px",
                        "top": iY + "px",
                        "width": iW - opts.posL + "px",
                        "padding-left": opts.posL + "px",
                        "height": iH + "px",
                        "line-height": iH + "px",
                        "color": opts.pColor,
                        "font-size": opts.pFont,
                        "z-index": opts.zIndex,
                        "cursor": "text"
                    }
                }).insertBefore($this);
 
            //初始状态就有内容
            var value = $this.val();
            if (value.length > 0) {
                oInput.hide();
            };
            var myEvent;
            if (isIE && version < 9) {
                myEvent = "propertychange";
            } else {
                myEvent = "input";
            }
            $this.bind(myEvent, function () {
                var value = $(this).val();
                if (value.length > 0) {
                    oInput.hide();
                } else {
                    oInput.show();
                }
            });
            oInput.click(function () {
                $this.trigger("focus");
            });
        });
    }
})(jQuery);