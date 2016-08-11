(function($) {
    $.loginController = function (options) {

        const opts = {
            loginModal:null
        };

        $.extend(opts, options);

        function login() {
            opts.loginModal.show();
        }

        return {
            login :login
        }
    }
}(jQuery))