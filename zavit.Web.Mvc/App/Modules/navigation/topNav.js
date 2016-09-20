import * as LoginModal from "../account/loginModal";
import * as AccountService from "../account/accountService";
import * as Routes from "../../routing/routes";

export function initialize() {
    refresh();
   
    $("#topnavLogin a").click((e) => {
        e.preventDefault();
        LoginModal.show(refresh);
    });

    $("#topnavLogout").click((e) => {
        e.preventDefault();
        AccountService.logOut();
        refresh();
        Routes.goTo(Routes.home);
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
        $("#topnavAccountDisplayName").text(userAccount.displayName);
    } else {
        $("#topnavAccount").hide();
        $("#topnavLogin").show();
    }
}