$('#geo-click').on('click', (ev) => {
    ev.preventDefault();
    let address = $('#address-input').val().replace(' ', '+');
    let url = 'https://maps.googleapis.com/maps/api/geocode/json?address=' + address + keys.GeocodingWebKey;
    $.ajax({
        method: 'GET',
        url: url,
        success: (data) => {
            if (data.status == 'OK') {
                let placeName = data.results[0].formatted_address,
                    location = data.results[0].geometry.location,
                    lat = location.lat,
                    long = location.lng;

                $('#search-result').html(placeName);
                $('#LocationName').val(placeName);
                $('#Latitude').val(lat);
                $('#Longitude').val(long);
            }
        },
        error: (err) => {
            console.log(err);
        }
    })
})