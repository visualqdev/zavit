import * as LoginModal from "../account/loginModal";
import * as AccountService from "../account/accountService";
import * as SideNav from "./sideNav";

export function initialize() {
    refresh();
   
    $("#topnavLogin a").click((e) => {
        e.preventDefault();
        LoginModal.show(refresh);
    });

    SideNav.initialize({
        onLogout: () => refresh()
    });

    $("#topnavShowSideNav a").click((e) => {
        e.preventDefault();
        SideNav.show();
    });
}

export function refresh() {
    const userAccount = AccountService.currentUserAccount();
    if (userAccount) {
        $("#topnavAccount").show();
        $("#topnavLogin").hide();
        $("#topnavAccount span").text(userAccount.displayName);
        $("#topnavShowSideNav").show();
    } else {
        $("#topnavAccount").hide();
        $("#topnavShowSideNav").hide();
        $("#topnavLogin").show();
    }
}