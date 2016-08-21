import * as AccountService from "./accountService";

const form = `
            <div class='modal fade' id='registerModal' tabindex='-1' role='dialog'>
                <div class='modal-dialog'>
                    <div class='registerModalContainer'>
                        <h2>Register</h2>
                        <br>
                        <p style="display: none;" id="registerWarning"></p>
                        <form id="registerSubmitForm">
                            <input type='text' id='registerDisplayName' placeholder='Display name'>
                            <input type='text' id='registerEmail' placeholder='Your email'>
                            <input type='password' id='registerPassword' placeholder='Password'>
                            <input type='submit' value='Register'>
                        </form>
                        <div class='registerHelp'>
                            <a href='#' id='registerLoginLink'>Login</a>
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
        $("#registerDisplayName").focus();
        $("#registerSubmitForm").submit((e) => {
            e.preventDefault();
            const displayName = modalForm.find("#registerDisplayName").val();
            const emailValue = modalForm.find("#registerEmail").val();
            const passwordValue = modalForm.find("#registerPassword").val();

            AccountService.register(displayName, emailValue, passwordValue)
                .then(() => {
                    modalForm.modal("hide");
                    userHasLoggedInCallback();
                })
                .catch((err) => {
                        const warning = $("#registerWarning");
                        warning.text(err);
                        warning.show();
                    }
                );
        });
    });

    modalForm.modal("show");
}