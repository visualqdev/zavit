(function($) {
    $.map = function(options) {
        var opts = {
            zoom: 12
        }

        $.extend(opts, options);

        function initialise(lat, lng) {
            var area = new google.maps.LatLng(lat, lng);

            var gMap = new google.maps.Map(document.getElementById('map'), {
                center: area,
                zoom: opts.zoom,
                scrollwheel: false
            });
        }

        return {
            initialise: initialise
        }
    }
}(jQuery));