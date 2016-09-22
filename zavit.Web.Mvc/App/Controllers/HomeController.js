import { Map
    

 } from "../modules/map/map";
import { Places } from "../modules/map/places";

export function explore(position) {

    const map = new Map({ executeWhenMapFullyLoaded: getPlaces});

    if (typeof position === 'undefined') {
        navigator.geolocation.getCurrentPosition(centerMapAtLocation, getDefaultMap);
    } 
    else {
        map.position = position;
        map.initialise();
    }

    function centerMapAtLocation(position) {
        map.position = position;
        map.initialise();
    }

    function getDefaultMap() {
        map.initialise();
    }

    function getPlaces() {
        const places = new Places({ map:map });
        places.initialise();
    }

}
