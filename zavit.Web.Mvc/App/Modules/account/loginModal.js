import * as AccountService from "./accountService";

const form = `
            <div class='modal fade' id='loginModal' tabindex='-1' role='dialog'>
                <div class='modal-dialog'>
                    <div class='loginModalContainer'>
                        <h2>Log In</h2>
                        <br>
                        <form>
                            <input type='text' id='loginEmail' placeholder='Your Email'>
                            <input type='password' id='loginPassword' placeholder='Password'>
                            <input type='button' id='loginSubmit' class='login loginmodal-submit' value='Log In'>
                        </form>
                        <div class='login-help'>
                            <a href='#' id='loginRegisterLink'>Register</a> - <a href='#' id='loginForgotPassword'>Forgot Password</a>
                        </div>
                    </div>
                </div>
            </div>`;

export function show() {
    let existingModal = $("#loginModal");
    
    if (existingModal.length > 0) {
        existingModal.modal("toggle");
    } else {
        let modalForm = $(form);
        modalForm.modal("show");
        modalForm.on("#loginSubmit", "click", () => {
            let emailValue = this.form.find("#loginEmail").val();
            let passwordValue = this.form.find("#loginPassword").val();

            AccountService.logIn(emailValue, passwordValue);
        });
    }
}