import * as VenueClient from "./venueClient";
import * as VenueMembershipClient from "./venueMembershipClient";
import * as AccountService from "../account/accountService";
import * as Routes from "../../routing/routes";
import * as PostLoginRedirect from "../account/postLoginRedirect";
import * as VenueJoiningStorage from "./venueJoiningStorage";

const joinVenueRedirectUrl = "/#/joinvenue";

export function getVenueAtPlace(placeId) {
    return new Promise((resolve, reject) => {
        VenueClient.getVenueAtPlace(placeId)
            .then(venue => resolve(venue));
    });
}

export function joinVenue(options) {
    if (AccountService.currentUserAccount()) {
        VenueClient
            .addVenue(options.placeId, null, options.activities)
            .then((venue) => letUserJoinVenue(venue.Id, options.activities))
            .catch();
    } else {
        PostLoginRedirect.storeRedirectUrl(joinVenueRedirectUrl);
        Routes.goTo(Routes.login);
        VenueJoiningStorage.storeOptions(options);
    }
}

function letUserJoinVenue(venueId, activities) {
    VenueMembershipClient
        .joinVenue(venueId, activities)
        .then(() => Routes.goTo(Routes.yourVenues));
}