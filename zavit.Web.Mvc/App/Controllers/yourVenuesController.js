import * as IndexView from "../views/yourVenues/indexView";
import * as YourVenuesMap from "../modules/venues/yourVenuesMap";
import * as VenueMembershipClient from "../modules/venues/venueMembershipClient";
import * as MainContent from "../layout/mainContent";
import * as PostLoginRedirect from "../modules/account/postLoginRedirect";
import * as Routes from "../routing/routes";

export function index() {
    MainContent.load(Routes.yourVenues);

    VenueMembershipClient
        .getVenueMemberships()
        .then(memberships => {
            const view = IndexView.getView(memberships);
            MainContent.append(view);
            YourVenuesMap.addMapToElements(".yourVenueMap");
        })
        .catch((error) => {
            if (error && error.status && error.status === 401) {
                PostLoginRedirect.storeRedirectUrl(window.location.href);
                Routes.goTo(Routes.login);
            }
        });
}