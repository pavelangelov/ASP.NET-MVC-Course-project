$('#add-lake').on('click', (ev) => {
    ev.preventDefault();
    let data = {
        name: $('#Name').val(),
        locationName: $('#LocationName').val(),
        latitude: $('#Latitude').val(),
        longitude: $('#Longitude').val(),
        info: $('#Info').val()
    };

    $.ajax({
        method: 'POST',
        url: '/moderator/lake/add',
        data: data,
        success: (response) => {
            let result = $('#result');
            if (response.status === 'success') {
                result.removeClass('text-danger')
                       .addClass('text-success')
                       .html(response.message);
            } else {
                result.addClass('text-danger')
                       .removeClass('text-success')
                       .html(response.message);
            }

            $('#result').show();
            setTimeout(() => {

                $('#result').hide();
            }, 3000);
        },
        error: (err) => {
            console.log('Error: ' + error.message);
        }
    });
});