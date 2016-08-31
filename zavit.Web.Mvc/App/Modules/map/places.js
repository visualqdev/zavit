import * as Progress from "../loading/progress";

export class Places {

    constructor(options = {}) {
        this.map = options.map || null,
        this.radius = options.radius || 3000;
    }

    initialise() {

        this.getPlaces();
        this.registerWithMapEvents();

        $('#loadPlaces').on("click", (e) => {
            e.preventDefault();
            this.removeMarkers();
            this.getPlaces();
            this.map.setZoom(this.map.zoom);
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
        places.forEach(place => this.addPlaceMarker(place));
        Progress.done();
    }

    addPlaceMarker(place) {
        this.map.addMarker({ lat: place.Latitude, lng: place.Longitude });
        this.map.addPlaceMarkerClickEvent(this.map.marker, this.showPlaceInfo, place, this.map.overlay);
    }

    showPlaceInfo(place, map) {
        const left = (map.markerPoint.x + -150) + "px",
            topPoint = (map.markerPoint.y - 195) + "px";

        $("#placeModal").remove();

        const placeModal = `<div id="placeModal" style="left:${left}; top:${topPoint};"> 
                                <h3>${place.Name}</h3>
                                <address>${place.Address}</address>
                            </div>`;

        $(placeModal).appendTo("#home");
    }

    removeMarkers() {
        this.map.markers.forEach(marker => this.map.removeMarker(marker));
    }
}