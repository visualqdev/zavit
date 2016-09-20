import * as ApiSettings from "../settings/apiSettings";

export function getPlaces(latitude, longitude, radius, name) {
    console.log(name);
    const placesUrl = `${ApiSettings.apiUrl}/api/places?latitude=${latitude}&longitude=${longitude}&radius=${radius}&name=${name}`;
    return new Promise((resolve, reject) => 
        $.ajax({
            url: placesUrl,
            type: "get",
            contentType: "application/json; charset=utf-8",
            success: resolve,
            error: reject
        })
    );
}