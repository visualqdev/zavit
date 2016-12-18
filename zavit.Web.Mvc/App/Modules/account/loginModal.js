import * as AccountService from "./accountService";
import * as RegisterModal from "./registerModal";
import * as ExternalAccountService from "./externalAccountService";
import * as LoginModalView from "../../views/account/loginModalView";

let isLoggingIn;

export function show(userHasLoggedInCallback) {
    const formView = LoginModalView.getView();
    const modalForm = $(formView);

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