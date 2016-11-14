import { Map } from "../modules/map/map";
import { Places } from "../modules/places/places";
import * as MainContent from "../layout/mainContent";
import * as Routes from "../routing/routes";
import * as IndexView from "../views/home/index";
import * as PlacesService from "../modules/places/placesService";
import * as Progress from "../modules/loading/progress";

let map;
let mapPlaces;

export function explore(position) {
    const view = IndexView.getView();
    MainContent.load(Routes.home, view);

    Progress.start();

    loadMap()
        .then(() => PlacesService.getPlaces({ map }))
        .then(places => {
            mapPlaces = new Places({
                map,
                getPlaces
            });
            mapPlaces.initialise();
            mapPlaces.addPlaces(places);

            Progress.done();
        });
}

function getPlaces() {
    Progress.start();

    PlacesService
        .getPlaces({ map })
        .then(places => {
            mapPlaces.addPlaces(places);

            Progress.done();
        });
}

function loadMap() {
    return new Promise((resolve, reject) => {
        const mapContainer = document.getElementById("exploreMap");
        map = new Map({
            executeWhenMapFullyLoaded: () => resolve(),
            mapContainer
        });

        if (typeof position === 'undefined') {
            navigator.geolocation.getCurrentPosition(centerMapAtLocation, getDefaultMap);
        } 
        else {
            map.position = position;
            map.initialise();
        }
    });
}

function centerMapAtLocation(position) {
    map.position = position;
    map.initialise();
}

function getDefaultMap() {
    map.initialise();
}
