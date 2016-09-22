import * as Progress from "../loading/progress";
import * as PlaceModal from "../places/placeModal";
import * as VenueModal from "../venues/venueModal";
import * as Search from "../navigation/search";
import * as Client from "../places/placesClient";
import * as Info from "../loading/info";
export class Places {

    constructor(options = {}) {
        this.map = options.map || null,
        this.radius = options.radius || 3000;
        this.name = "";
    }

    initialise() {

        this.getPlaces();
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

        $("#home").delegate("#placeModal a[data-nextMarker]", "click", (e) => {
            e.preventDefault();
            const marker = this.map.markers[$(e.currentTarget).attr("data-nextMarker")];
            this.map.triggerMarkerClick(marker);
        });

        $("#home").delegate("#placeModal a[data-prevMarker]", "click", (e) => {
            e.preventDefault();
            const marker = this.map.markers[$(e.currentTarget).attr("data-prevMarker")];
            this.map.triggerMarkerClick(marker);
        });

        $("#home").delegate("#placeModal #placeModalBeAvailable", "click", (e) => {
            e.preventDefault();
            this.clearPlaceInfo();
            const marker = this.map.markers[$(e.currentTarget).attr("data-marker-index")],
                placeId = $(e.currentTarget).attr("data-place-id");
            VenueModal.show({
                markerX: marker.map.markerPoint.x,
                markerY: marker.map.markerPoint.y,
                placeId,
                map: this.map
            });
        });

        $(window).on("resize",() => { this.map.pannedBy = { x: 0, y: 0 };});

        $("html").css("overflow", "hidden");
    }

    getPlaces() {
        Progress.start();
        return new Promise((resolve, reject) => {
            Client.getPlaces(this.map.position.coords.latitude, this.map.position.coords.longitude, this.radius, this.name).then(places=> resolve(places));
        })
        .then(places => this.addPlaces(places));
    }

    registerWithMapEvents() {
        this.map.dragged.push(this.clearPlaceInfo);
        this.map.zoomed.push(this.clearPlaceInfo);
    }

    clearPlaceInfo() {
        $("#placeModal").remove();
        $("[data-name=placeModal]").remove();
    }

    addPlaces(places) {
        if (places.length) {
            places.forEach((place, placeIndex) => this.addPlaceMarker(place, placeIndex, places.length));
            Progress.done();
            this.map.triggerMarkerClick(this.map.markers[0]);
        } else {
            Info.provide("Sorry, no results where found!");
            Progress.done();
        }
    }

    addPlaceMarker(place, placeIndex, amountOfPlaces) {
        this.map.addMarker({ lat: place.Latitude, lng: place.Longitude });
        this.map.addPlaceMarkerClickEvent(this.map, this.showPlaceInfo, place, placeIndex, amountOfPlaces);
    }

    showPlaceInfo(place, placeIndex, amountOfPlaces, map) {
        $("[data-name=placeModal]").remove();
        const placeModal = PlaceModal.modal(place, placeIndex, amountOfPlaces, map);
        $(placeModal).appendTo("#home");
    }

    removeMarkers() {
        this.map.markers.forEach(marker => this.map.removeMarker(marker));
        this.map.markers = [];
    }
}