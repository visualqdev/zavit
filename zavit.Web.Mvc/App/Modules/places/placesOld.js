import * as PlacePopup from "../places/placePopup";
import * as Search from "../navigation/search";
import * as Info from "../loading/info";
import * as Routes from "../../routing/routes";

export class Places {

    constructor(options = {}) {
        this.map = options.map || null,
        this.radius = options.radius || 3000;
        this.getPlaces = options.getPlaces;
        this.onPlaceSelected = options.onPlaceSelected;
    }

    initialise() {
        this.registerWithMapEvents();
        this.registerPlaceEvents();
        Search.initialise(this);
    }

    registerPlaceEvents() {
        $("#exploreMap").delegate("#placeModal #placeModalBeAvailable", "click", (e) => {
            e.preventDefault();
            this.clearPlaceInfo();
            const button = $(e.currentTarget),
                placeId = button.attr("data-place-id"),
                venueId = button.attr("data-venue-id");

            if (venueId && venueId > 0)
                Routes.goTo(`${Routes.yourVenue}/${venueId}`);
            else if (placeId) {
                Routes.goTo(`${Routes.yourVenue}?placeid=${placeId}`);
            }
        });

        $(window).on("resize",() => { this.map.pannedBy = { x: 0, y: 0 };});
    }

    loadPlacesAtCurrentLocation() {
        this.removeMarkers();
        this.getPlaces();
        this.map.setZoom(this.map.zoom);
    }

    registerWithMapEvents() {
        this.map.dragged.push(this.clearPlaceInfo);
        this.map.zoomed.push(this.clearPlaceInfo);
    }

    clearPlaceInfo() {
        $("[data-name=placeModal]").remove();
    }

    selectPlace(dataMarkerIndex) {
        const marker = this.map.markers[dataMarkerIndex];
        this.map.triggerMarkerClick(marker);
    }

    addPlaces(places) {
        if (places.length) {
            places.forEach((place, placeIndex) => this.addPlaceMarker(place, placeIndex, places.length));
            this.map.triggerMarkerClick(this.map.markers[0]);
        } else {
            Info.provide("Sorry, no results where found!");
        }
    }

    addPlaceMarker(place, placeIndex, amountOfPlaces) {
        this.map.addMarker({ lat: place.Latitude, lng: place.Longitude });
        this.map.addPlaceMarkerClickEvent(
            this.map, 
            this.showPlaceInfo,
            {
                place, 
                placeIndex, 
                amountOfPlaces,
                onPlaceSelected: this.onPlaceSelected
            });
    }

    showPlaceInfo(callbackOptions, map) {
        PlacePopup.show({
            place: callbackOptions.place,
            placeIndex: callbackOptions.placeIndex,
            placesMap: map
        });
            
        if (callbackOptions.onPlaceSelected) {
            callbackOptions.onPlaceSelected(callbackOptions.placeIndex);
        }
    }

    removeMarkers() {
        this.map.markers.forEach(marker => this.map.removeMarker(marker));
        this.map.markers = [];
    }
}