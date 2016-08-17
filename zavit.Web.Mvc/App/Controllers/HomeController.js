import { Map } from '../modules/map/map';

var Map = new Map();

export function explore() {

    navigator.geolocation.getCurrentPosition(centerMapAtLocation);

    function centerMapAtLocation(position) {
        Map.initialise(position.coords.latitude, position.coords.longitude);
    }
}
