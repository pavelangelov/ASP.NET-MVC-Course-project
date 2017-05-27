'use strict';

$('#add').on('click', (ev) => {
    ev.preventDefault();
    let name = $('#LocationName').val(),
        latitude = $('#Latitude').val(),
        longitude = $('#Longitude').val(),
        info = $('#Info').val();

    $.ajax({
        method: "POST",
        url: '/moderator/location/add',
        data: {
            name,
            latitude,
            longitude,
            info
        },
        success: (response) => {
            if (response.status == 'success') {
                $('#result').removeClass('text-danger')
                            .addClass('text-success')
                            .html(response.message);
            } else {
                $('#result').addClass('text-danger')
                            .removeClass('text-success')
                            .html(response.message);
            }

            $('#result').show();

            setTimeout(() => {

                $('#result').hide();
            }, 3000);
        },
        error: (err) => {
            console.log(err.message);
        }
    });
})