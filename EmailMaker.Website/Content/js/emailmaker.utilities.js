(function ($) {
    $.postJson = function (url, data, success) {
        $.ajax({
            type: "POST",
            url: url,
            data: JSON.stringify(data),
            contentType: "application/json; charset=utf-8",
            success: success
        });
    };
})(jQuery);
