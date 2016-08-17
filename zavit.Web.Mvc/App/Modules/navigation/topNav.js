import * as LoginModal from "../account/loginModal";

export function initialize() {
    $("#topnavLogin").click(() => {
        LoginModal.show();
    });
}