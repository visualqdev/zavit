(function($) {
    $.homeController = function() {

        return {
            explore: function() {
                navigator.geolocation.getCurrentPosition(this.centerMapAtLocation);
            },
            centerMapAtLocation: function(position) {
                $.map().initialise(position.coords.latitude, position.coords.longitude);
            }
        };
    };
}(jQuery));