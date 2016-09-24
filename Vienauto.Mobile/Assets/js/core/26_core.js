(function ($) {
    $.fn.activeDeactiveTarget = function ($activeTarget, $deActiveTarget, $callbackMethod, className) {
        this.on("click", function () {
            if (typeof $activeTarget != 'undefined' && typeof $activeTarget != null && !$activeTarget.hasClass(className))
                $activeTarget.addClass(className);

            if (typeof $deActiveTarget != 'undefined' && typeof $deActiveTarget != null && $deActiveTarget.hasClass(className))
                $deActiveTarget.removeClass(className);

            if (typeof $callbackMethod != 'undefined' && typeof $callbackMethod != null)
                $callbackMethod();
        });
    };

}(jQuery));
