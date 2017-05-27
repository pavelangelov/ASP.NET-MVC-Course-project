'use strict';

$('.container').on('click', '.load-comments', (ev) => {
    $('#comment-form').hide();
    $('#comments').show();
    let name = $(ev.target).attr('data-name'),
        page = $(ev.target).attr('data-page');

    let container = $('#comments');

    if (name == undefined) {
        return;
    }
    $.ajax({
        method: 'GET',
        url: `/lakes/getComments?name=${name}&page=${page}`,
        success: (data) => {
            container.html(data);
        },
        error: (err) => {
            console.log(err.message)
        }
    });
})