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
            if (response.status === "success") {
                $('#result').addClass('text-success')
                            .removeClass('text-danger')
                            .text(response.message);
            } else {
                $('#result').addClass('text-danger')
                            .removeClass('text-success')
                            .text(response.message);
            }
            $('#result').show();

            setTimeout(() => {

                $('#result').hide();
            }, 3000);
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