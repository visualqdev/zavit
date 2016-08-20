import * as LoginModal from "../account/loginModal";
import * as AccountService from "../account/accountService";

export function initialize() {
    refresh();
   
    $("#topnavLogin a").click((e) => {
        e.preventDefault();
        LoginModal.show(refresh);
    });
}

export function refresh() {
    const userAccount = AccountService.currentUserAccount();
    if (userAccount) {
        $("#topnavAccount").show();
        $("#topnavLogin").hide();
        $("#topnavAccount a").text(userAccount.displayName);
    } else {
        $("#topnavAccount").hide();
        $("#topnavLogin").show();
    }
}