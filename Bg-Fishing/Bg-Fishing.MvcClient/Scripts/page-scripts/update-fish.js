'use strict';

$('#add').on('click', (ev) => {
    ev.preventDefault();

    updateFish('addFish');
});

$('#remove').on('click', (ev) => {
    ev.preventDefault();

    updateFish('removeFish');
});

function updateFish(updateMethod) {
    var selectedLake = $('#SelectedLake').val();

    var selectedFish = [];
    $('#SelectedFish :selected').each((i, selected) => {
        selectedFish[i] = $(selected).text();
    });

    if (selectedLake.length && selectedFish.length > 0) {
        $.ajax({
            method: 'POST',
            url: '/moderator/lake/' + updateMethod,
            data: {
                selectedLake,
                selectedFish
            },
            success: (response) => {
                if (response.status === "success") {
                    $('#result').addClass('text-success')
                                .removeClass('text-danger')
                                .text(response.message);
                } else {
                    $('#result').removeClass('text-success')
                            .addClass('text-danger')
                            .text(response.message);
                }

                $('#result').show();

                hideResult();
            },
            error: (err) => {
                console.log(err.message);
            }
        });
    } else {
        $('#result').removeClass('text-success')
                            .addClass('text-danger')
                            .text('Не е избран язовир.');
        $('#result').show();

        hideResult();
    }
}

function hideResult() {
    setTimeout(() => {
        $('#result').hide();
    }, 3000);
}