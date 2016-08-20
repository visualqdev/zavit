import * as LoginModal from "../modules/account/loginModal";
import * as AccountService from "../modules/account/accountService";
import * as TopNav from "../modules/navigation/topNav";

export function login() {
    LoginModal.show();
}

export function logout() {
    AccountService.logOut();
    TopNav.initialize();
}