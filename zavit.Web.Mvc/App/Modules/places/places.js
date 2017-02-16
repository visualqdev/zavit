import * as PlacePopup from "../places/placePopup";
import * as Search from "../navigation/search";
import * as Info from "../loading/info";
import * as Routes from "../../routing/routes";
import * as Geocode from "../map/geocode";

let map;
let onPlaceSelected;
let getPlaces;

export function initialise(options) {
    map = options.map || null;
    getPlaces = options.getPlaces;
    onPlaceSelected = options.onPlaceSelected;
    
    registerWithMapEvents();
    registerPlaceEvents();
    Search.initialise({
        sarchForArea,
        searchForVenue
    });
}

export function addPlaces(places) {
    if (places.length) {
        places.forEach((place, placeIndex) => addPlaceMarker(place, placeIndex, places.length));
        map.triggerMarkerClick(map.markers[0]);
    } else {
        Info.provide("Sorry, no results where found!");
    }
}

export function loadPlacesAtCurrentLocation() {
    removeMarkers();
    getPlaces();
    map.setZoom(map.zoom);
}

export function selectPlace(dataMarkerIndex) {
    const marker = map.markers[dataMarkerIndex];
    map.triggerMarkerClick(marker);
}

export function removeMarkers() {
    map.markers.forEach(marker => map.removeMarker(marker));
    map.markers = [];
    clearPlaceInfo();
}

function sarchForArea(areaName) {
    function load(position) {

        removeMarkers();
        map.map.setCenter(new google.maps.LatLng(position.coords.latitude, position.coords.longitude));
        map.position = position;
        getPlaces();
    }

    if (areaName !== "") Geocode.getGeoCodeByAddress(areaName, load);
}

function searchForVenue(venueName) {
    removeMarkers();
    getPlaces(venueName);
}

function addPlaceMarker(place, placeIndex, amountOfPlaces) {
    map.addMarker({ lat: place.Latitude, lng: place.Longitude });
    map.addPlaceMarkerClickEvent(
        map, 
        showPlaceInfo,
        {
            place, 
            placeIndex, 
            amountOfPlaces,
            onPlaceSelected: onPlaceSelected
        });
}

function showPlaceInfo(callbackOptions, map) {
    PlacePopup.show({
        place: callbackOptions.place,
        placeIndex: callbackOptions.placeIndex,
        placesMap: map
    });
            
    if (callbackOptions.onPlaceSelected) {
        callbackOptions.onPlaceSelected(callbackOptions.placeIndex);
    }
}

function registerPlaceEvents() {
    $("#exploreMap").delegate("#placeModal #placeModalBeAvailable", "click", (e) => {
        e.preventDefault();
        clearPlaceInfo();
        const button = $(e.currentTarget),
            placeId = button.attr("data-place-id"),
            venueId = button.attr("data-venue-id");

        if (venueId && venueId > 0)
            Routes.goTo(`${Routes.yourVenue}/${venueId}`);
        else if (placeId) {
            Routes.goTo(`${Routes.yourVenue}?placeid=${placeId}`);
        }
    });

    $(window).on("resize",() => { map.pannedBy = { x: 0, y: 0 };});
}

    

function registerWithMapEvents() {
    map.dragged.push(clearPlaceInfo);
    map.zoomed.push(clearPlaceInfo);
}

function clearPlaceInfo() {
    $("[data-name=placeModal]").remove();
}