import { Map } from '../modules/map/map';



export function explore() {

    var map = new Map({zoom:17});

    navigator.geolocation.getCurrentPosition(centerMapAtLocation);

    function centerMapAtLocation(position) {
        map.initialise(position.coords.latitude, position.coords.longitude);
    }
}
