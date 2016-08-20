import * as AccountService from "./accountService";
import * as RegisterModal from "./registerModal";

const form = `
            <div class='modal fade' id='loginModal' tabindex='-1' role='dialog'>
                <div class='modal-dialog'>
                    <div class='loginModalContainer'>
                        <h2>Log In</h2>
                        <br>
                        <p style="display: none;" id="loginWarning">The Email or Password does not match</p>
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

export function show(userHasLoggedInCallback) {
    const modalForm = $(form);

    modalForm.on("hidden.bs.modal", () => {
        modalForm.remove();
    });
    modalForm.on("shown.bs.modal", () => {
        $("#loginSubmitForm").submit((e) => {
            e.preventDefault();
            const emailValue = modalForm.find("#loginEmail").val();
            const passwordValue = modalForm.find("#loginPassword").val();

            AccountService.logIn(emailValue, passwordValue)
                .then(() => {
                    modalForm.modal("hide");
                    userHasLoggedInCallback();
                })
                .catch(() => {
                        $("#loginWarning").show();
                    }
                );
        });
        $("#loginRegisterLink").click((e) => {
            e.preventDefault();
            modalForm.modal("hide");
            RegisterModal.show(userHasLoggedInCallback);
        });
    });

    modalForm.modal("show");
}