import * as ApiSettings from "../settings/apiSettings";
import * as AuthorizedClient from "../clients/authorizedClient";

export function getVenueAtPlace(placeId) {
    const venuesUrl = `${ApiSettings.apiUrl}api/places/${placeId}/venues/default`;

    return new Promise((resolve, reject) => 
        $.ajax({
            url: venuesUrl,
            type: "get",
            contentType: "application/json; charset=utf-8",
            success: resolve,
            error: reject
        })
    );
}

export function addVenue(placeId, name, activities) {
    const postVenueUrl = `${ApiSettings.apiUrl}/api/places/${placeId}/venues`;

    const data = {
        activities,
        name
    };

    return new Promise((resolve, reject) =>
        AuthorizedClient
            .send({
                url: postVenueUrl,
                type: "post",
                contentType: "application/json; charset=utf-8",
                data: JSON.stringify(data)
            })
            .then(resolve));
}