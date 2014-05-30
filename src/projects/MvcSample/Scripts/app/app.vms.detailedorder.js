(function(app, ko) {
    app.vms.DetailedOrder = function() {
        var self = this;

        this.customers = ko.observableArray();
        this.selectedCustomer = ko.observable();

        $.getJSON("/customers", function(data) {
            self.customers(data);
        });
    };

    if ($('#detailedOrder').length > 0) {
        app.vms.bind('detailed-order', app.vms.DetailedOrder, 'detailedOrder');
    }
})(app, ko);