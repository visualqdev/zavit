import * as IndexView from "../views/yourVenues/index";
import * as YourVenueMap from "../modules/venues/yourVenueMap";
import * as VenueMembershipClient from "../modules/venues/venueMembershipClient";

export function index() {
    const container = $("#mainContent");
    container.empty();

    VenueMembershipClient
        .getVenueMemberships()
        .then(memberships => {
            const view = IndexView.getView(memberships);
            container.append(view);
            YourVenueMap.addMapToElements(".yourVenueMap");
        });
}