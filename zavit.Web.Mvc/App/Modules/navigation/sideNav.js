import * as AccountService from "../account/accountService";

export function initialize(options) {
    const opts = {
        onLogout: () => {}
    };
    $.extend(opts, options);

    $("#sidenavClose").click((e) => {
        e.preventDefault();
        close();
    });

    $("#sidenavLogout").click((e) => {
        e.preventDefault();
        AccountService.logOut();
        opts.onLogout();
        close();
    });
}

export function show() {
    $("#sidenav").css("right", "0");
}

function close() {
    const sidenav = $("#sidenav");
    const sidenavWidth = sidenav.css("width");
    sidenav.css("right", `-${sidenavWidth}`);
}