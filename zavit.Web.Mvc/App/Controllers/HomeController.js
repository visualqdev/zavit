import { Map } from "../modules/map/map";
import * as Places from "../modules/places/places";
import * as MainContent from "../layout/mainContent";
import * as Routes from "../routing/routes";
import * as IndexView from "../views/home/index";
import * as ExploreListPartial from "../views/home/exploreListPartial";
import * as Progress from "../modules/loading/progress";
import * as VenueService from "../modules/venues/venueService";
import * as HomeLayout from "../modules/home/homeLayout";

let map;

export function explore(position) {
    const view = IndexView.getView();
    MainContent.load(Routes.home, view);

    Progress.start();

    loadMap()
        .then(() => VenueService.getVenues({ map }))
        .then(venues => {
            
            Places.initialise({
                map,
                getPlaces,
                onPlaceSelected: venueSelected
            });

            createExploreList(venues);
            Places.addPlaces(venues);

            HomeLayout.showLoadPlacesHere({
                onLoadPlaces: Places.loadPlacesAtCurrentLocation
            });

            Progress.done();
        });
}

function getPlaces(venueName) {
    Progress.start();

    VenueService.getVenues({
        map,
        name: venueName
    })
    .then(venues => {
        createExploreList(venues);
        Places.addPlaces(venues);
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
    $("#exploreList").on("click", "li.exploreListVenue", function(e) {
        e.preventDefault();
        e.stopPropagation();
        $("#exploreList li.selected").removeClass("selected");
        const venueItem = $(this);
        venueItem.addClass("selected");
        Places.selectPlace(venueItem.attr("data-marker-index"));
    });
}

function venueSelected(index) {
    $("#exploreList li.selected").removeClass("selected");
    const venueItem = $(`#exploreList li[data-marker-index='${index}']`);
    venueItem.addClass("selected");

    const exploreList = $("#exploreList");
    exploreList.scrollTop(exploreList.scrollTop() + venueItem.position().top
        - exploreList.height()/2 + venueItem.height()/2);
}
