'use strict';

$('#galleries-select').on('change', (ev) => {
    let value = $(ev.target).val(),
        defaultValue = $('select#galleries-select option:first').val();

    if (value && value != defaultValue) {
        $.get(`/api/images/unconfirmed/${value}`, (data) => {
            let images = JSON.parse(data).result,
                fragment = $('<div />');

            images.forEach(i => {
                $('<div />').addClass('col-sm-3 thumbnail')
                    .append($('<img />').attr('src', i['ImageUrl']))
                    .append($('<button />').addClass('btn btn-success')
                        .text('Потвърди')
                        .attr('img-id', i['Id']))
                    .appendTo(fragment);
            });

            $('#images-container').html(fragment);
        })
    }
})

$('#images-container').on('click', 'button', (ev) => {
    ev.preventDefault();
    let id = $(ev.target).attr('img-id');
    if (id && id.length) {
        let token = $('input[name=__RequestVerificationToken]').val();
        $(`button[img-id=${id}]`).attr('disabled', 'disabled');
        $.ajax({
            method: 'PUT',
            url: `/pictures/confirm`,
            data: {
                imageId: id,
                __RequestVerificationToken: token
            },
            success: (response) => {
                let res = JSON.parse(response),
                    message = $('<div />').addClass('response')
                        .text(res.message);

                if (res.status == 'success') {
                    message.addClass('text-success');
                } else {
                    message.addClass('text-danger');
                }

                $(ev.target).parent().html(message);
            },
            error: (err) => {
                console.log(err);
            }
        })
    }
})