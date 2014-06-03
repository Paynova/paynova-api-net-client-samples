app = (function (ko, $) {
    var app = {
        bindings: {},
        vms: {
            bind: function (context, vmFn, elementId) {
                var bindings = app.bindings;

                bindings[context] = new vmFn($.connection);

                ko.applyBindings(bindings[context], $('#' + elementId).get(0));

                return bindings[context];
            }
        }
    };

    return app;
})(ko, $);