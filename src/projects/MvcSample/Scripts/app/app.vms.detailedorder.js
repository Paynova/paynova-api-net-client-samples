(function(app, ko) {
    app.vms.DetailedOrder = function() {
        this.customers = ko.observableArray();
        this.selectedCustomer = ko.observable();
    };

    var customers = $('#detailedOrder').data('customers');
    if (customers) {
        var vm = app.vms.bind('detailed-order', app.vms.DetailedOrder, 'detailedOrder');
        vm.customers(customers);
    }
})(app, ko);