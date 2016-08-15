(function ($) {
    $.loginModal = function (options) {

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
		    "</div>"].join(""));

        var opts = {
        };

        $.extend(opts, options);

        return {
            show: function () {
                var existingModal = $("#loginModal");
                if (existingModal.length > 0) {
                    existingModal[0].modal("toggle");
                } else {
                    this.form.modal("show");
                    this.form.on("#loginSubmit", "click", this.submitLogin);
                }
            },
            form: form,
            submitLogin: function () {
                var emailValue = this.form.find("#loginEmail").val();
                var passwordValue = this.form.find("#loginPassword").val();

                $.accountService().logIn(emailValue, passwordValue);
            }
        }
    }
}(jQuery))