import * as LoginModal from "../account/loginModal";
import * as AccountService from "../account/accountService";

export function initialize() {
    var userAccount = AccountService.currentUserAccount();
    
    if (userAccount) {
        $("#topnavAccount").show();
        $("#topnavLogin").hide();
        $("#topnavAccount a").text(userAccount.email);
    } else {
        $("#topnavAccount").hide();
        $("#topnavLogin").show();
    }
   
    $("#topnavLogin a").click(() => {
        LoginModal.show();
    });
}