(function($) {
    $.validator.methods.number = function(value, element) {
        if ($.global.parseFloat(value)) {
            return true;
        }
        return false;
    };

    $.extend($.validator.methods, {
        range: function (value, element, param) {
            var val = $.global.parseFloat(value);
            return this.optional(element) || (val >= param[0] && val <= param[1]);
        }
    });
})(jQuery);