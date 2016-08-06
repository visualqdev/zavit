import * as LoginModal from "../Modules/account/loginModal"

export function login(name) {
    return LoginModal.show(name);
}