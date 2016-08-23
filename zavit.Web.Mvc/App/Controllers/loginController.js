(function($) {
    $.loginController = function(options) {

        var opts = {};

        $.extend(opts, options);

        function login() {
            $.loginModal().show();
        }

        return {
            login: login
        }
    }
}(jQuery));