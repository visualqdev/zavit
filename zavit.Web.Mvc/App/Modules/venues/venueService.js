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

export function getVenue(venueId) {
    return new Promise((resolve, reject) => {
        VenueClient.getVenue(venueId)
            .then(venue => resolve(venue));
    });
}

export function joinVenue(options) {
    if (AccountService.currentUserAccount()) {
        if (options.venueId) {
            letUserJoinVenue(options);
        } else {
            VenueClient
                .addVenue(options.placeId, null, options.activities)
                .then((venue) => {
                    options.venueId = venue.Id;
                    letUserJoinVenue(options);
                })
                .catch((error) => {
                    if (error && error.status && error.status === 401) {
                        redirectToLogin(options);
                    }
                });
        }
    } else {
        redirectToLogin(options);
    }
}

function letUserJoinVenue(options) {
    VenueMembershipClient
        .joinVenue(options.venueId, options.activities)
        .then(() => Routes.goTo(`${Routes.yourVenue}/${options.venueId}`))
        .catch((error) => {
            if (error && error.status && error.status === 401) {
                redirectToLogin(options);
            }
        });
}

function redirectToLogin(options) {
    PostLoginRedirect.storeRedirectUrl(joinVenueRedirectUrl);
    VenueJoiningStorage.storeOptions(options);
    Routes.goTo(Routes.login);
}