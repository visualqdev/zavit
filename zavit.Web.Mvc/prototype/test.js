var gMarkers = [];
var gPlaces = [];
var firstPlace;
var overlay;
var map;
var left;
var topPoint;
var count =0;

$(function(){
	 var startPos;
	 var geoSuccess = function(position) {
		startPos = position;
		initialize(startPos.coords.latitude,startPos.coords.longitude);
	 }
	navigator.geolocation.getCurrentPosition(geoSuccess);
	$('#home').delegate('#nextGym','click', function(e){
		  e.preventDefault();
		  var clickCount = getCount();
		  google.maps.event.trigger(gMarkers[clickCount],'click', gMarkers[clickCount].position);
	  });
	$('#home').delegate('#prevGym','click', function(e){
		  e.preventDefault();
		  var clickCount = getPrevCount();
		  google.maps.event.trigger(gMarkers[clickCount -1],'click', gMarkers[clickCount -1].position);
	  });

	$('#searchBtn').on("click", function(e){
	  e.preventDefault();
	  $(this).parent().toggleClass('searchOpen', 300);
	});

	$('#home').delegate('#gymInfo button', "click", function(e){
	  e.preventDefault();
	  gMarkers.forEach(removeMarker);
	  map.setZoom(14);
	  map.panBy(345, 0);
	  openLocationHighlight();
	});

});
function showPanel(){
  $('#members').show().css({'top': $('svg rect').position().top, 'left': $('svg rect').position().left});
  $('#members #mask').show().animate({'width': 0, 'left': 520}, 300);
}
function openLocationHighlight(){
  
  $('<svg id="lh"><defs></defs></svg>').hide().appendTo('#home').attr('id', 'locationHighlight').fadeIn('slow',  showPanel);
  var s = Snap('#locationHighlight');
  var rectangle = s.rect(0, 0, 800, 400, 0, 0).attr({'fill':'#333000'});
  var group = s.group();
  group.rect(0, 0, 800, 400 ,0, 0).attr({'fill':'#fff'});
  s.rect(50, 50, 200, 200, 0, 0).attr({'fill':'black'}).appendTo(group);
  var heading = s.text(50, 30, gPlaces[getCount() -1].name);
  heading.attr({'fill' : '#ccc','fontFamily': 'arial', 'fontSize':'16px'});
  rectangle.attr({'mask':group})

  $('#gymInfo').remove();
  $('#searchBtn').parent().hide();

  
}
function removeMarker(marker, index){
  if(index !== count -1)
  marker.setMap(null);
}
function getCount(){
  var countI = count;
  count++;

  return countI;
}
function getPrevCount(){
  count--;
  return count;
}

function initialize(lat, long) {
	
  var area = new google.maps.LatLng(lat, long);

    map = new google.maps.Map(document.getElementById('map'), {
    center: area,
    zoom: 12,
    scrollwheel: false
  });

    google.maps.event.addListener(map, 'projection_changed', function () {
        overlay = new google.maps.OverlayView();
        overlay.draw = function () {};
        overlay.setMap(map);
  });
  google.maps.event.addListener(map,'drag', function() {
      $('#gymInfo').remove();
  });

  // Specify location, radius and place types for your Places API search.
  var request = {
    location: area,
    radius: '5000',
    types: ['gym']
  };

  var service = new google.maps.places.PlacesService(map);

  service.nearbySearch(request, function(results, status) {
    if (status == google.maps.places.PlacesServiceStatus.OK) {

      for (var i = 0; i < results.length; i++) {
        var place = results[i];
        firstPlace = results[0];
        // If the request succeeds, draw the place location on
        // the map as a marker, and register an event to handle a
        // click on the marker.
        var marker = new google.maps.Marker({
          map: map,
          position: place.geometry.location
        });
        
        addClick(marker, place, i);
        gMarkers.push(marker);
        gPlaces.push(place);
      }
      map.setCenter(new google.maps.LatLng(firstPlace.geometry.location.lat(), firstPlace.geometry.location.lng()));
      google.maps.event.trigger(gMarkers[0],'click', gMarkers[getCount()].position);
    }
  });
  
}

function addClick(marker, place, int){
 
  google.maps.event.addListener(marker, 'click', function(e) {
    
    var point
    if(e.latLng){
      count = int +1;
      map.setCenter(e.latLng);
       point = overlay.getProjection().fromLatLngToContainerPixel(e.latLng);

    }
    else {
      var latLng = new google.maps.LatLng(e.lat(), e.lng());
      map.setCenter(latLng);
      point = overlay.getProjection().fromLatLngToContainerPixel(latLng);
    }
   
    left = (point.x + 20)+ 'px';
    topPoint = (point.y - 60) + 'px';

    $('#gymInfo').remove();

    var info = $('<\div>').attr('id', 'gymInfo').css({'left':left,'top':topPoint}).appendTo("#home");
    $('<\span>').attr('id', 'linkInfo').appendTo('#gymInfo').text(count + ' of ' + gMarkers.length);
    if(count !== gMarkers.length)
      $('<\a>').appendTo('#linkInfo').attr('id', 'nextGym').attr('href', '#').text('next');
    $('<\h3>').appendTo('#gymInfo').text(place.name);
    $('<\p>').appendTo('#gymInfo').text(place.vicinity);
    if(count > 1){
      $('<\a>').prependTo('#linkInfo').attr('id', 'prevGym').attr('href', '#').text('prev ');
    }
    $('<button type="submit" class="btn btn-default"><strong>4 players</strong> you can play here</button>').appendTo('#gymInfo');
    $('<button type="submit" class="btn btn-default">Be available to play here</button>').appendTo('#gymInfo');
    $('<button type="submit" class="btn btn-default">Invite a friend to play here</button>').appendTo('#gymInfo');
  });
}

//google.maps.event.addDomListener(window, 'load', initialize);
