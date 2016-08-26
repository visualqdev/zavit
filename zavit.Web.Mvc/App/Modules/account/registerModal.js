﻿import * as AccountService from "./accountService";
import * as ExternalAccountService from "./externalAccountService";

const form = `
            <div class='modal fade' id='registerModal' tabindex='-1' role='dialog'>
                <div class='modal-dialog'>
                    <div class='registerModalContainer'>
                        <h2>Register</h2>                        
                        <p style="display: none;" id="registerWarning"></p>
                        <div>
                            <a id="registerFacebook" href="#" class="btn">Register with Facebook</a>
                            <a id="registerGoogle" href="#" class="btn">Register with Google</a>
                        </div>
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

let isRegistering;

export function show(userHasLoggedInCallback) {
    const modalForm = $(form);

    modalForm.on("hidden.bs.modal", () => {
        modalForm.remove();
    });
    modalForm.on("shown.bs.modal", () => {
        $("#registerDisplayName").focus();
        $("#registerSubmitForm").submit((e) => {
            e.preventDefault();

            if (isRegistering === true) return;

            onRegisteringStarted();
            const displayName = modalForm.find("#registerDisplayName").val();
            const emailValue = modalForm.find("#registerEmail").val();
            const passwordValue = modalForm.find("#registerPassword").val();

            AccountService.register(displayName, emailValue, passwordValue)
                .then(() => {
                    modalForm.modal("hide");
                    userHasLoggedInCallback();
                    onRegisteringCompleted();
                })
                .catch((err) => {
                        const warning = $("#registerWarning");
                        warning.text(err);
                        warning.show();
                        onRegisteringCompleted();
                    }
                );
        });
        $("#registerFacebook").click((e) => {
            e.preventDefault();
            ExternalAccountService.facebookLogin();
        });
        $("#registerGoogle").click((e) => {
            e.preventDefault();
            ExternalAccountService.googleLogin();
        });
    });

    modalForm.modal("show");
}

function onRegisteringStarted() {
    isRegistering = true;
}

function onRegisteringCompleted() {
    isRegistering = false;
}