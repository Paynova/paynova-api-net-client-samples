(function(app, ko) {
    app.vms.TopNav = function(cn) {
        var self = this,
            callbacksHub = cn.callbacksHub;

        self.callbackCount = ko.observable(0);

        $.get('/callbacks/count', function(data) {
            self.callbackCount(data);
        });

        callbacksHub.client.notifyCallbackCount = function (newCount) {
            self.callbackCount(newCount);
        };
    };
})(app, ko);