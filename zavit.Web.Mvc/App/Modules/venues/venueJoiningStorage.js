import * as Storage from "../storage/storage";

const venueJoiningOptionsKey = "venue_joining_options_key";

export function storeOptions(options) {
    Storage.storeSessionObject(venueJoiningOptionsKey, options);
}

export function getOptions() {
    return Storage.getSessionObject(venueJoiningOptionsKey);
}