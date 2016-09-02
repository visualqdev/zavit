import * as Progress from "../loading/progress";
import * as PlaceModal from "../map/placeModal";

export class Places {

    constructor(options = {}) {
        this.map = options.map || null,
        this.radius = options.radius || 3000;
    }

    initialise() {

        this.getPlaces();
        this.registerWithMapEvents();
        this.registerPlaceEvents();
    }

    registerPlaceEvents() {

        $('#loadPlaces').on("click", (e) => {
            e.preventDefault();
            this.map.markers.forEach(marker => this.map.removeMarker(marker));
            this.map.markers = [];
            this.getPlaces();
            this.map.setZoom(this.map.zoom);
        });

        $('#home').delegate("#placeModal a[data-nextMarker]", "click", (e) => {
            e.preventDefault();
            const marker = this.map.markers[$(e.currentTarget).attr("data-nextMarker")];
            this.map.triggerMarkerClick(marker);
        });

        $('#home').delegate("#placeModal a[data-prevMarker]", "click", (e) => {
            e.preventDefault();
            const marker = this.map.markers[$(e.currentTarget).attr("data-prevMarker")];
            this.map.triggerMarkerClick(marker);
        });
    }

    getPlaces() {
        const latitude = this.map.position.coords.latitude,
            longitude = this.map.position.coords.longitude,
            url = `/api/places?latitude=${latitude}&longitude=${longitude}&radius=${this.radius}`;

        Progress.start();

        fetch(url).then(response => {  return response.json(); }).then(places => this.addPlaces(places));
    }

    registerWithMapEvents() {
        this.map.dragged.push(this.clearPlaceInfo);
        this.map.zoomed.push(this.clearPlaceInfo);
    }

    clearPlaceInfo() {
        $('#placeModal').remove();
    }

    addPlaces(places) {
        places.forEach((place, placeIndex) => this.addPlaceMarker(place, placeIndex, places.length));
        Progress.done();
        this.map.triggerMarkerClick(this.map.markers[0]);
    }

    addPlaceMarker(place, placeIndex, amountOfPlaces) {
        this.map.addMarker({ lat: place.Latitude, lng: place.Longitude });
        this.map.addPlaceMarkerClickEvent(this.map, this.showPlaceInfo, place, placeIndex, amountOfPlaces);
    }

    showPlaceInfo(place, placeIndex, amountOfPlaces, map) {
        const placeModal = PlaceModal.modal(place, placeIndex, amountOfPlaces, map);
        $(placeModal).appendTo("#home");
    }
}