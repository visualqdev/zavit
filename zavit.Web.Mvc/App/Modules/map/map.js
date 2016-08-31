export class Map {

    constructor(options = {}) 
    {
        this.zoom = options.zoom || 14;
        this.executeWhenMapFullyLoaded = options.executeWhenMapFullyLoaded || null;
        this.marker = null;
        this.markerPoint = null;
        this.overlay = null;
        this.position = options.position || { coords: { latitude: 51.508530, longitude: -0.076132 } };
        this.dragged = [];
        this.zoomed = [];
        this.markers = [];
    }
   
    initialise() {
        
        const area = new google.maps.LatLng(this.position.coords.latitude, this.position.coords.longitude);

        const map = new google.maps.Map(document.getElementById("map"), {
            center: area,
            zoom: this.zoom,
            scrollwheel: true
        });
        
        const projectionChanged = new Promise(function(resolve) {
            google.maps.event.addListener(map,
                "projection_changed",
                function() {
                    this.overlay = new google.maps.OverlayView();
                    this.overlay.draw = function() {};
                    this.overlay.setMap(map);
                    resolve();
                }
           );
        });

        const mapHasLoaded = new Promise(function(resolve) {
            google.maps.event.addListenerOnce(map, "idle", () => resolve());
        });

        Promise.all([projectionChanged, mapHasLoaded])
            .then(value => {
                this.executeWhenMapFullyLoaded();
                this.map = map;
            }, 
            reason => {
                console.log(reason);
            }
        );

        google.maps.event.addListener(map, "drag", () => {
            this.dragged.forEach(func => func());
            this.updatePosition(this.map.center);
        });

        google.maps.event.addListener(map, "zoom_changed", () => {
            this.zoomed.forEach(func => func());
            this.updatePosition(this.map.center);
        });
    }

    setZoom(level) {
        this.map.setZoom(level);
    }

    updatePosition(mapCenter) {
        this.position = { coords: { latitude: mapCenter.lat(), longitude: mapCenter.lng()} };
    }

    addMarker(latLng) {

        this.marker = new google.maps.Marker({
            map: this.map,
            position: latLng
        });

        this.markers.push(this.marker);
    }

    removeMarker(marker) {
        marker.setMap(null);
    }

    addPlaceMarkerClickEvent(marker, callback, place) {
        google.maps.event.addListener(marker, "click", function(e) {
            this.map.setCenter(e.latLng);
            this.map.markerPoint = this.map.overlay.getProjection().fromLatLngToContainerPixel(e.latLng);
            callback(place, this.map);
        });
    }

}
