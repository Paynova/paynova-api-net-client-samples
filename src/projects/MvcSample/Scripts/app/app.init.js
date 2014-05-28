(function (app, $) {
    var init = function () {
        if ($.global) {
            var data = $("meta[name='accept-language']").attr("content");
            if (data)
                $.global.preferCulture(data);
        }

        app.vms.bind('topNav', app.vms.TopNav, 'header-section');

        $.connection.hub.start();
    };

    $(init);
})(app, jQuery);