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
    const existingModal = $("#loginModal");
    if (existingModal.length > 0) {
        existingModal.modal("toggle");
    } else {
        const modalForm = $(form);
        
        modalForm.on("shown.bs.modal",() => $("#loginSubmit").click(() => {
            const emailValue = modalForm.find("#loginEmail").val();
            const passwordValue = modalForm.find("#loginPassword").val();
        
            AccountService.logIn(emailValue, passwordValue)
                .then(() => {
                    modalForm.modal("toggle");
                });
        }));

        modalForm.modal("show");
    }
}