export function getGeoCodeByAddress(address, callback) {

    const geocoder = new google.
s.Geocoder();

    geocoder.geocode({'address': address}, function(results, status) 
    {   
        if (status == google.maps.GeocoderStatus.OK) {
            const position = {
                coords: {
                    latitude: results[0].geometry.location.lat(),
                    longitude: results[0].geometry.location.lng()
                }
            };
            callback(position);
        }
    });
}