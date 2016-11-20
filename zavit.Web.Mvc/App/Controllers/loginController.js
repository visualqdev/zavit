import * as LoginModal from "../modules/account/loginModal";
import * as AccountService from "../modules/account/accountService";
import * as TopNav from "../modules/navigation/topNav";
import * as PostLoginRedirect from "../modules/account/postLoginRedirect";

export function login() {
    LoginModal.show(userHasLoggedIn);
}

export function logout() {
    AccountService.logOut();
    TopNav.refresh();
}

function userHasLoggedIn() {
    PostLoginRedirect.processRedirect();
    TopNav.refresh();
}