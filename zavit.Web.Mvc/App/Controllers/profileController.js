﻿import * as IndexView from "../views/profile/indexView";
import * as Routes from "../routing/routes";
import * as MainContent from "../layout/mainContent";
import * as ProfileClient from "../modules/profile/profileClient";
import * as ProfileDetailsLayout from "../modules/profile/profileDetailsLayout";
import * as PostLoginRedirect from "../modules/account/postLoginRedirect";

export function index() {
    MainContent.load(Routes.profile);

    ProfileClient
        .getProfile()
        .then(profile => {
            const view = IndexView.getView(profile);
            MainContent.append(view);
            attachEditEvents();
        })
        .catch((error) => {
            if (error && error.status && error.status === 401) {
                PostLoginRedirect.storeRedirectUrl(window.location.href);
                Routes.goTo(Routes.login);
            }
        });
}

function attachEditEvents() {
    ProfileDetailsLayout.attachEvents();

    ProfileDetailsLayout.onValueChanged((propertyName, value) => {
        
    });
}