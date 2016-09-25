import { Map } from "../map/map";

export function addMapToElements(jquerySelector) {
    $(jquerySelector).each((index, element) => {
        const container = $(element);
        const lat = container.attr("data-venue-lat");
        const lng = container.attr("data-venue-lng");

        const map = new Map({
            thisMapIsFixed: true,
            position: {
                coords: {
                    latitude: lat, 
                    longitude: lng
                }
            },
            mapContainer: element,
            executeWhenMapFullyLoaded: addMarker
        });

        function addMarker() {
            map.addMarker({
                lat: parseFloat(lat), 
                lng: parseFloat(lng)
            });
        }

        map.initialise();
    });
}