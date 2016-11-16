import { Map } from "../modules/map/map";
import { Places } from "../modules/places/places";
import * as MainContent from "../layout/mainContent";
import * as Routes from "../routing/routes";
import * as IndexView from "../views/home/index";
import * as ExploreListPartial from "../views/home/exploreListPartial";
import * as Progress from "../modules/loading/progress";
import * as VenueService from "../modules/venues/venueService";

let map;
let mapPlaces;

export function explore(position) {
    const view = IndexView.getView();
    MainContent.load(Routes.home, view);

    Progress.start();

    loadMap()
        .then(() => VenueService.getVenues({ map }))
        .then(venues => {
            mapPlaces = new Places({
                map,
                getPlaces
            });
            mapPlaces.initialise();
            mapPlaces.addPlaces(venues);

            createExploreList(venues);

            Progress.done();
        });
}

function getPlaces() {
    Progress.start();

    VenueService.getVenues({ map })
        .then(venues => {
            mapPlaces.addPlaces(venues);
            createExploreList(venues);
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

function createExploreList(venues) {
    const exploreListView = ExploreListPartial.getView(venues);
    $("#exploreList").html(exploreListView);
}
