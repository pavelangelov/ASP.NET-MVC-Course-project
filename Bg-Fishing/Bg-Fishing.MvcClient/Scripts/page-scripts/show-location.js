function myMap() {
    var mapOptions = {
        center: new google.maps.LatLng(lat, long),
        zoom: 12,
        mapTypeId: google.maps.MapTypeId.HYBRID
    }
    var map = new google.maps.Map(document.getElementById("map"), mapOptions);
}