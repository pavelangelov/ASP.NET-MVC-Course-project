$('#submit-btn').on('click', (ev) => {
    ev.preventDefault();
    $('#comments').hide();
    let content = $('#comment-content').val(),
        name = $(ev.target).attr('data-name');
    $.ajax({
        method: 'POST',
        url: '/lakes/' + name + '/addComment',
        data: {
            lakeName: name,
            content: content
        },
        success: (response) => {
            console.log(response.message);
        },
        error: (err) => {
            console.log(err);
        }
    })
})

$('#show-comment-form').on('click', (ev) => {
    $('#comments').hide();
    $('#comment-form').show();
})