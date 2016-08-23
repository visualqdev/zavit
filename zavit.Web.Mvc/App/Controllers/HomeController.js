import { Map } from "../modules/map/map";
import { Places } from "../modules/map/places"

export function explore() {

    let _position;

    navigator.geolocation.getCurrentPosition(centerMapAtLocation);

    const map = new Map({ executeWhenMapFullyLoaded: getPlaces});

    function centerMapAtLocation(position) {
        _position = position;
        map.initialise(position.coords.latitude, position.coords.longitude);
    }

    function getPlaces() {
        const places = new Places({ map:map, position: _position });
        places.getPlaces();
    }

}
