import * as AccountService from "./accountService";
import * as RegisterModal from "./registerModal";
import * as ExternalAccountService from "./externalAccountService";

const form = `
            <div class='modal fade' id='loginModal' tabindex='-1' role='dialog'>
                <div class='modal-dialog'>
                    <div class='loginModalContainer'>
                        <h2>Log In</h2>
                        <br>
                        <p style="display: none;" id="loginWarning">The Email or Password does not match</p>
                         <div class="socialLogins">
                            <a id="loginFacebook" class="btn btn-block btn-social btn-facebook">
                                <i class="fa fa-facebook fa-l"></i> Log In with Facebook
                            </a>
                            <a id="loginGoogle" class="btn btn-block btn-social btn-google-plus">
                                <i class="fa fa-google-plus fa-l"></i> Log In with Google
                            </a>                        
                        </div>
                        <form id="loginSubmitForm">
                            <input type='text' id='loginEmail' placeholder='Your Email'>
                            <input type='password' id='loginPassword' placeholder='Password'>
                            <input type='submit' id='loginSubmit' class='login loginmodal-submit' value='Log In'>
                        </form>
                        <div class='loginHelp'>
                            <a href='#' id='loginRegisterLink'>Register</a> - <a href='#' id='loginForgotPassword'>Forgot Password</a>
                        </div>
                    </div>
                </div>
            </div>`;

let isLoggingIn;

export function show(userHasLoggedInCallback) {
    const modalForm = $(form);

    modalForm.on("hidden.bs.modal", () => {
        modalForm.remove();
    });
    modalForm.on("shown.bs.modal", () => {
        $("#loginEmail").focus();
        $("#loginSubmitForm").submit((e) => {
            e.preventDefault();

            if (isLoggingIn === true) return;

            onLoginStarted();
            const emailValue = modalForm.find("#loginEmail").val();
            const passwordValue = modalForm.find("#loginPassword").val();

            AccountService.logIn(emailValue, passwordValue)
                .then(() => {
                    modalForm.modal("hide");
                    userHasLoggedInCallback();
                    onLoginCompleted();
                })
                .catch(() => {
                    $("#loginWarning").show();
                    onLoginCompleted();
                });
        });
        $("#loginFacebook").click((e) => {
            e.preventDefault();
            ExternalAccountService.facebookLogin();
        });
        $("#loginGoogle").click((e) => {
            e.preventDefault();
            ExternalAccountService.googleLogin();
        });
        $("#loginRegisterLink").click((e) => {
            e.preventDefault();
            modalForm.modal("hide");
            RegisterModal.show(userHasLoggedInCallback);
        });
    });

    modalForm.modal("show");
}

function onLoginStarted() {
    isLoggingIn = true;
}

function onLoginCompleted() {
    isLoggingIn = false;
}