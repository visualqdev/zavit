(function($) {
    $.homeController = function() {

        return {
            explore: function() {
                navigator.geolocation.getCurrentPosition(this.centerMapAtLocation);
            },
            centerMapAtLocation: function(position) {
                $.map().initialise(position.coords.latitude, position.coords.longitude);
            }
        };
    };
}(jQuery));
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
(function() {
    crossroads.addRoute("/", function () {
        $.homeController().explore();
    });

    crossroads.addRoute("/login", function () {
        $.loginController().login();
    });

    function parseHash(newHash, oldHash) {
        crossroads.parse(newHash);
    }

    hasher.initialized.add(parseHash);
    hasher.changed.add(parseHash);
    hasher.init();
}());
(function($) {
    $.accountService = function(options) {
        var opts = {
        
        };

        $.extend(opts, options);

        function logIn(email, password) {

        };

        return {
            logIn: logIn
        }
    }
}(jQuery));
(function($) {
    $.loginModal = function(options) {

        var form = $([
            "<div class='modal fade' id='loginModal' tabindex='-1' role='dialog'>",
            "<div class='modal-dialog'>",
            "<div class='loginModalContainer'>",
            "<h2>Log In</h2><br>",
            "<form>",
            "<input type='text' id='loginEmail' placeholder='Your Email'>",
            "<input type='password' id='loginPassword' placeholder='Password'>",
            "<input type='button' id='loginSubmit' class='login loginmodal-submit' value='Log In'>",
            "</form>",
            "<div class='login-help'>",
            "<a href='#' id='loginRegisterLink'>Register</a> - <a href='#' id='loginForgotPassword'>Forgot Password</a>",
            "</div>",
            "</div>",
            "</div>",
            "</div>"
        ].join(""));

        var opts = {
        
        };

        $.extend(opts, options);

        return {
            show: function() {
                var existingModal = $("#loginModal");
                if (existingModal.length > 0) {
                    existingModal[0].modal("toggle");
                } else {
                    this.form.modal("show");
                    this.form.on("#loginSubmit", "click", this.submitLogin);
                }
            },
            form: form,
            submitLogin: function() {
                var emailValue = this.form.find("#loginEmail").val();
                var passwordValue = this.form.find("#loginPassword").val();

                $.accountService().logIn(emailValue, passwordValue);
            }
        }
    }
}(jQuery));
(function($) {
    $.map = function(options) {
        var opts = {
            zoom: 12
        }

        $.extend(opts, options);

        function initialise(lat, lng) {
            var area = new google.maps.LatLng(lat, lng);

            var gMap = new google.maps.Map(document.getElementById('map'), {
                center: area,
                zoom: opts.zoom,
                scrollwheel: false
            });
        }

        return {
            initialise: initialise
        }
    }
}(jQuery));