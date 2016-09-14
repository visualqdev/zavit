import * as ApiSettings from "../settings/apiSettings";

export function getVenueAtPlace(placeId) {
    const venuesUrl = `api/places/${placeId}/venues/default`;

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