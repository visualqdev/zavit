import * as VenueClient from "./venueClient";
import * as VenueMembershipClient from "./venueMembershipClient";
import * as AccountService from "../account/accountService";
import * as Routes from "../../routing/routes";
import * as PostLoginRedirect from "../account/postLoginRedirect";
import * as VenueJoiningStorage from "./venueJoiningStorage";

const joinVenueRedirectUrl = "/#/joinvenue";

export function getVenues(options) {
    const radius = options.radius || 3000,
        name = options.name || "";

    return VenueClient.getVenues(
        options.map.position.coords.latitude,
        options.map.position.coords.longitude,
        radius, name);
}

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
    return new Promise((resolve, reject) => {
        if (AccountService.currentUserAccount()) {
            if (options.venueId && options.venueId > 0) {
                VenueMembershipClient.joinVenue(options.venueId, options.activities)
                .then(result => {
                        resolve(result);
                    })
                .catch((error) => rejectOrAuthorize(error, reject));
            } else {
                VenueClient
                    .addVenue(options.placeId, null, options.activities)
                    .then((venue) => {
                        options.venueId = venue.Id;
                        return VenueMembershipClient.joinVenue(options.venueId, options.activities);
                    })
                    .then(() => Routes.goTo(`${Routes.yourVenue}/${options.venueId}`))
                    .catch((error) => rejectOrAuthorize(error, reject));
            }
        } else {
            redirectToLogin(options);
            reject();
        } 
    });
}

function rejectOrAuthorize(error, reject) {
    if (error && error.status && error.status === 401) {
        redirectToLogin(options);
    } else {
        reject(error);
    }
}

function redirectToLogin(options) {
    PostLoginRedirect.storeRedirectUrl(joinVenueRedirectUrl);
    VenueJoiningStorage.storeOptions(options);
    Routes.goTo(Routes.login);
}