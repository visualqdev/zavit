import * as VenueService from "../modules/venues/venueService";
import * as VenueJoiningStorage from "../modules/venues/venueJoiningStorage";

export function joinVenue(venueId, activities) {
    const options = VenueJoiningStorage.getOptions();
    VenueService.joinVenue(options);
}