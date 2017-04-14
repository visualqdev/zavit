import * as LoginModal from "../account/loginModal";
import * as AccountService from "../account/accountService";
import * as Routes from "../../routing/routes";
import { htmlEncode } from "../htmlUtils/htmlEncoder";
import * as InfoModal from "../../views/navigation/infoModalView";
import * as Storage from "../storage/storage";

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

    $(".infoButton").on("click", (e) => {
        e.preventDefault();
        const infoView = InfoModal.getView();
        const infoModal = $(infoView);
        infoModal.modal("show");
    });

    $(".navbar-collapse a:not('.dropdown-toggle')").click (() => $(".navbar-collapse").collapse('hide'));

    let isUsersFirstVisit = Storage.getString("hasVisited") === null;
    if (isUsersFirstVisit) {
        $(".infoButton").trigger("click");
        Storage.storeObject("hasVisited", true);
    }
}

export function refresh() {
    const userAccount = AccountService.currentUserAccount();
    if (userAccount) {
        $("#topnavAccount").show();
        $("#topnavLogin").hide();
        $("#topnavAccountDisplayName").text(htmlEncode(userAccount.displayName));
    } else {
        $("#topnavAccount").hide();
        $("#topnavLogin").show();
    }
}

export function navigatedToRoute(routeName) {
    $("#topnavLinks li.active").removeClass("active");
    $(`#topnavLinks li[data-page='${routeName}']`).addClass("active");
}