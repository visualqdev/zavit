import * as IndexView from "../views/yourVenues/index";
import * as YourVenueMap from "../modules/venues/yourVenueMap";
import * as VenueMembershipClient from "../modules/venues/venueMembershipClient";
import * as MainContent from "../layout/mainContent";
import * as PostLoginRedirect from "../modules/account/postLoginRedirect";
import * as Routes from "../routing/routes";

export function index() {
    MainContent.load();

    VenueMembershipClient
        .getVenueMemberships()
        .then(memberships => {
            const view = IndexView.getView(memberships);
            MainContent.append(view);
            YourVenueMap.addMapToElements(".yourVenueMap");
        })
        .catch((error) => {
            if (error && error.status && error.status === 401) {
                PostLoginRedirect.storeRedirectUrl(window.location.href);
                Routes.goTo(Routes.login);
            }
        });
}