import { Map } from "../modules/map/map";
import { Places } from "../modules/map/places"

export function explore() {

    const map = new Map({ executeWhenMapFullyLoaded: getPlaces});

    navigator.geolocation.watchPosition(centerMapAtLocation, map.initialise());

    function centerMapAtLocation(position) {
        map.position = position;
        map.initialise();
    }

    function getPlaces() {
        const places = new Places({ map:map });
        places.getPlaces();
    }

}
