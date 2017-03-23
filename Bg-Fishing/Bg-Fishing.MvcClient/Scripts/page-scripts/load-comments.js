$('#load-comments').on('click', (ev) => {
    $('#comment-form').hide();
    $('#comments').show();
    let name = $(ev.target).attr('data-name'),
        page = $(ev.target).attr('data-page');

    $.ajax({
        method: 'GET',
        url: '/lakes/' + name + '/comments?page=' + page,
        success: (data) => {
            loadComments(data.comments);
        },
        error: (err) => {
            console.log(err);
        }
    })
})

function loadComments(commetnsArr) {
    let container = $('#comments');
    container.html('');
    commetnsArr.forEach((c) => {
        let commentContainer = $('<li />').addClass('list-group-item');
        let user = $('<div />').addClass('text-primary')
                                .html('Изпратено от: ' + c.Username)
                                .appendTo(commentContainer);

        let commentSeparator = $('<hr />').appendTo(commentContainer);
        let commentContent = $('<div />').html(c.Content).appendTo(commentContainer);

        commentContainer.appendTo(container);
    })
}