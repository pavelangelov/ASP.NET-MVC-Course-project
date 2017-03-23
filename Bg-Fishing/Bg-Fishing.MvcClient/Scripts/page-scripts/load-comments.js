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
        let date = $('<div />').addClass('text-success').html('Дата: ' + new Date(parseInt(c.PostedDate.substr(6))).toLocaleDateString()).appendTo(commentContainer);

        commentContainer.appendTo(container);
    })
}