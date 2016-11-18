import * as PlaceModal from "../places/placeModal";
import * as VenueModal from "../venues/venueModal";
import * as Search from "../navigation/search";
import * as Info from "../loading/info";
export class Places {

    constructor(options = {}) {
        this.map = options.map || null,
        this.radius = options.radius || 3000;
        this.name = "";
        this.getPlaces = options.getPlaces;
        this.onPlaceSelected = options.onPlaceSelected;
    }

    initialise() {
        this.registerWithMapEvents();
        this.registerPlaceEvents();
        Search.initialise(this);
    }

    registerPlaceEvents() {
        $("#loadPlaces").on("click", (e) => {
            e.preventDefault();
            this.removeMarkers();
            this.getPlaces();
            this.map.setZoom(this.map.zoom);
        });

        $("#exploreMap").delegate("#placeModal a[data-nextMarker]", "click", (e) => {
            e.preventDefault();
            showPlace($(e.currentTarget).attr("data-nextMarker"));
        });

        $("#exploreMap").delegate("#placeModal a[data-prevMarker]", "click", (e) => {
            e.preventDefault();
            showPlace($(e.currentTarget).attr("data-prevMarker"));
        });

        $("#exploreMap").delegate("#placeModal #placeModalBeAvailable", "click", (e) => {
            e.preventDefault();
            this.clearPlaceInfo();
            const button = $(e.currentTarget),
                marker = this.map.markers[button.attr("data-marker-index")],
                placeId = button.attr("data-place-id"),
                venueId = button.attr("data-venue-id");
            VenueModal.show({
                markerX: marker.map.markerPoint.x,
                markerY: marker.map.markerPoint.y,
                placeId,
                venueId,
                map: this.map
            });
        });

        $(window).on("resize",() => { this.map.pannedBy = { x: 0, y: 0 };});
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
        PlaceModal.show(callbackOptions.place, callbackOptions.placeIndex, callbackOptions.amountOfPlaces, map);
        if (callbackOptions.onPlaceSelected) {
            callbackOptions.onPlaceSelected(callbackOptions.placeIndex);
        }
    }

    removeMarkers() {
        this.map.markers.forEach(marker => this.map.removeMarker(marker));
        this.map.markers = [];
    }
}