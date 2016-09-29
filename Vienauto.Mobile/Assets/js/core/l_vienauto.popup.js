var myHeight;
if (typeof (window.innerWidth) == "number") {
    myWidth = window.innerWidth;
    myHeight = window.innerHeight;
}
else if (document.documentElement && (document.documentElement.clientWidth || document.documentElement.clientHeight)) {
    myWidth = document.documentElement.clientWidth;
    myHeight = document.documentElement.clientHeight;
}
else if (document.body && (document.body.clientWidth || document.body.clientHeight)) {
    myWidth = document.body.clientWidth;
    myHeight = document.body.clientHeight;
}

(function ($) {
    $.fn.creatNewTabed_Pinter = function (settings) {
        var config = {
            "w": 650,
            "title": "",
            "link": "",
            "code": "",
            "af": function () { }
        };
        var settings = $.extend(config, settings);
        return this.each(function () {
            var _w = settings.w;
            var _title = settings.title;
            var _link = settings.link;
            var _af = settings.af;
            var _code = settings.code;
            var _html = "<div id='zoomScroll'><div class='fixtomid' style='width:" + _w + "px'><div class='zoom' style='width:" + _w + "px'><div class='header'><div class='fl' style='width:" + (_w - 70) + "px'><h2>" + _title + "</h2></div><div class='fl close'><span></span></div><div class='cl'></div></div><div class='content'><div class='sc-loading-process'></div></div></div></div></div>";
            $("body").prepend(_html);
            $("body").addClass("noscroll");
            if (_code != "") {
                $("#zoomScroll .zoom .content").html(_code);
                _af();
            } else {
                $("#zoomScroll .zoom .content").load(_link, function () {
                    _af();
                });
            }
            $(document).unbind("keyup");
            $(document).keyup(function (e) {
                if (e.keyCode == 27) {
                    $("#zoomScroll").remove();
                    $("body").removeClass("noscroll");
                    window_location_newhash('#refesh');
                }
            });
            $("#zoomScroll .zoom .header .close span").click(function () {
                $("#zoomScroll").remove();
                $("body").removeClass("noscroll");
                window_location_newhash('#refesh');
            });
        });
    }
})(jQuery);

(function ($) {
    $.fn.creatNewTabed_Pintercustom = function (settings) {
        var config = {
            "w": 650,
            "h": 300,
            "title": "",
            "link": "",
            "code": "",
            "af": function () { }
        };
        var settings = $.extend(config, settings);
        return this.each(function () {
            var _w = settings.w;
            var _h = settings.h;
            var _title = settings.title;
            var _link = settings.link;
            var _af = settings.af;
            var _code = settings.code;
            var _the_height = ((myHeight - _h) * 50 / 100);
            var _html = "<div id='zoomScroll' ><div class='fixtomid' style='width:";
            _html = _html.concat(_w).concat("px; height:").concat(_h).concat("px;margin-top:").concat(_the_height).concat("px;'>");
            _html = _html.concat("<div class='zoom1' style='width:").concat(_w).concat("px; height:").concat(_h).concat("px;'><div class='header'>");
            _html = _html.concat("<div class='fl' style='width:").concat((_w - 70)).concat("px'><h2>").concat(_title);
            _html = _html.concat("</h2></div><div class='fl close'><span></span></div>");
            _html = _html.concat("<div class='cl'></div></div><div class='content'><div class='sc-loading-process'></div></div></div></div></div>");
            $("body").prepend(_html);
            $("body").addClass("noscroll");
            if (_code != "") {
                $("#zoomScroll .zoom1 .content").html(_code);
                _af();
            } else {
                $("#zoomScroll .zoom1 .content").load(_link, function () {
                    _af();
                });
            }
            $(document).unbind("keyup");
            $(document).keyup(function (e) {
                if (e.keyCode == 27) {
                    $("#zoomScroll").remove();
                    $("body").removeClass("noscroll");
                    window_location_newhash('#refesh');
                }
            });
            $("#zoomScroll .zoom1 .header .close span").click(function () {
                $("#zoomScroll").remove();
                $("body").removeClass("noscroll");
                window_location_newhash('#refesh');
            });
        });
    }
})(jQuery);