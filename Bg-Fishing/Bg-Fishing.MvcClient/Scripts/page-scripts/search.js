$('#search-btn').on('click', (ev) => {
    ev.preventDefault();
    let value = $('#search-value').val();
    if (value !== undefined && value.length) {
        $.ajax({
            method: 'POST',
            url: '/api/search/lakes',
            data: { name: value },
            success: (data) => {
                ShowLakes(data);
            },
            error: (err) => {
                console.log(err);
            }
        })
    }
})

function ShowLakes(lakesArr) {
    let container = $('#lakes-result');
    container.html('');

    if (lakesArr.length) {
        container.css('display', 'block');
    } else {
        container.css('display', 'none');
    }

    lakesArr.forEach((lake) => {
        let lakeContainer = $('<li />'),
            link = $('<a />').attr('href', '/lakes/' + lake.Name)
                                .html('яз. ' + lake.Name)
                                .appendTo(lakeContainer);

        container.append(lakeContainer);
    })
}