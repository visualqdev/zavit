import * as IndexView from "../views/yourVenues/index";
import * as YourVenueMap from "../modules/venues/yourVenueMap";
import * as VenueMembershipClient from "../modules/venues/venueMembershipClient";
import * as MainContent from "../layout/mainContent";

export function index() {
    MainContent.load();

    VenueMembershipClient
        .getVenueMemberships()
        .then(memberships => {
            const view = IndexView.getView(memberships);
            MainContent.append(view);
            YourVenueMap.addMapToElements(".yourVenueMap");
        });
}