(function () {
    $(function () {
        var $body = $('body');
        var ajaxError, ajaxSuccess, submitClicked;
        submitClicked = null;
        $body.on('click', 'form :submit', function () {
            return submitClicked = this;
        });
        $body.on('submit', 'form.ajax-container, .ajax-container form', function (event) {
            var $form, $submitClicked, $submitShim, blockUI;
            event.preventDefault();
            $form = $(this);
            blockUI = $form.hasClass('js-block-ui');
            if (submitClicked != null) {
                $submitClicked = $(submitClicked);
                $submitClicked.prop('disabled', true);
                $submitClicked.data('normal-text', $submitClicked.text());
                if ($submitClicked.data["loading-text"] !== 'undefined')
                    $submitClicked.text($submitClicked.data["loading-text"]);
            }
            (function (submitClicked) {
                return $form.ajaxSubmit({
                    beforeSubmit: function (e, form) {
                        if (blockUI) {
                            App.blockUI({
                                boxed: true,
                                message: "Please wait..."
                            });
                        }
                        return $form.trigger('beforesubmit', e);
                    },
                    success: function (response) {
                        if (blockUI) {

                        }
                        if ($submitClicked != null) {
                            $(submitClicked).text($(submitClicked).data('normal-text'));
                            $submitClicked.prop('disabled', false);
                        }
                        Ajax.ajaxSuccess(response, $form);
                        if (response.success)
                            $form[0].reset();
                    },
                    error: function (response) {
                        if (blockUI) {

                        }
                        if ($submitClicked != null) {
                            $(submitClicked).text($(submitClicked).data('normal-text'));
                            $submitClicked.prop('disabled', false);
                        }
                        Ajax.ajaxError(response, $form);
                    }
                });
            })(submitClicked);
            submitClicked = null;
            return false;
        });

    });
}).call(this);

///declare an Ajax object to handle binding event to a specified link, form dynamically.
///for example: you have a request to server and recevie a json object. the json object will be parsed into html elements (such as <a>)
///but, at this time, the $document ready has passed over, so that those html elements won't have handlers for click event when user clicks <a>

var Ajax = function () {
    var _ajaxError = function (response, $target) {
        if (typeof window.handleAjaxError === "function")
            return window.handleAjaxError(response);
        return $target.trigger('ajaxError', [response]);
    };

    var _ajaxSuccess = function (response, $target) {
        var $ajaxContainer, $html, e;
        if (response.success) {
            if (typeof window.hideAllModals === "function") {
                window.hideAllModals();
            }
            if ((response.message != null) && response.message.length > 0) {
                if (typeof window.notificationInfo === "function")
                    window.notificationSuccess(response.message);
            }
        } else {
            if ((response.message != null) && response.message.length > 0) {
                if (typeof window.notificationError === "function")
                    window.notificationError(response.message);
            }
        }
        _ajaxUpdateHtmlForIds(response, $target);
        _ajaxRedirectTo(response, $target);
        return $target.trigger('ajaxSuccess', [response]);
    };

    var _ajaxUpdateHtmlForIds = function (response, $target) {
        if (response.htmlForIds != null) {
            var $html, fragment, id, _results;
            _results = [];
            for (id in response.htmlForIds) {
                $html = $(response.htmlForIds[id]);
                $('#' + id).replaceWith($html);
                $.validator.unobtrusive.parse($html);
                _results.push($('body').trigger('ajaxUpdateHtmlForIds', [id, $html, response]));
            }
            return _results;
        };
    };

    var _ajaxRedirectTo = function (response, $target) {
        if (response.redirect)
            window.location.replace(response.redirect);
    };

    return {
        ajaxSuccess: function (response, $target) {
            return _ajaxSuccess(response, $target);
        },

        ajaxError: function (response, $target) {
            return _ajaxError(response, $target);
        },

        handleAjaxLink: function ($container, ids) {
            $container.find(ids).on("click", function (event) {
                event.preventDefault();
                var $a = $(this);
                return $.ajax({
                    url: $a.attr("href"),
                    type: 'post',
                    success: function (response) {
                        return _ajaxSuccess(response, $a);
                    },
                    error: _ajaxError
                });
            });
        },

        bind: function (url, param, $target, eventCallback) {
            $.ajax({
                url: url,
                data: param,
                type: 'get',
                beforeSend: function () {
                },
                success: function (response) {
                    eventCallback($target, response);
                },
                error: function (response) {
                    return _ajaxError;
                }
            });
        },

        post: function ($elem, $postDataCallback) {
            $elem.on("click", function (event) {
                event.preventDefault();
                var $button = $(this);

                var postData = null;
                if (typeof $postDataCallback != 'undefined')
                    postData = $postDataCallback();

                return $.ajax({
                    url: $button.data("href"),
                    data: postData,
                    type: 'post',
                    beforeSend: function () {
                    },
                    success: function (response) {
                        return _ajaxSuccess(response, $elem);
                    },
                    error: function (response) {
                        return _ajaxError;
                    }
                });
            });
        },

        postOnChange: function ($elem, $postDataCallback) {
            $elem.on("change", function (event) {
                event.preventDefault();
                var $select = $(this);

                var postData = null;
                if (typeof $postDataCallback != 'undefined')
                    postData = $postDataCallback();

                return $.ajax({
                    url: $select.data("href"),
                    data: postData,
                    type: 'post',
                    beforeSend: function () {
                    },
                    success: function (response) {
                        return _ajaxSuccess(response, $elem);
                    },
                    error: function (response) {
                        return _ajaxError;
                    }
                });
            });
        }
    }
}();


