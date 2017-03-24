$('#load-comments').on('click', (ev) => {
    $('#comment-form').hide();
    $('#comments').show();
    let name = $(ev.target).attr('data-name'),
        page = $(ev.target).attr('data-page');

    $.ajax({
        method: 'GET',
        url: '/api/comments?name=' + name + '&page=' + page,
        success: (data) => {
            loadComments(data);
        },
        error: (err) => {
            console.log(err);
        }
    })
})

function loadComments(commentsArr) {
    let container = $('#comments');
    container.html('');
    if (commentsArr === undefined || !commentsArr.length) {
        let message = $('<li />').addClass('list-group-item')
                                    .addClass('text-danger')
                                    .addClass('text-center')
                                    .html('Все още няма изпратени мнения за този язовир.')
                                    .appendTo(container);

        return;
    }

    commentsArr.forEach((c) => {
        let commentContainer = $('<li />').addClass('list-group-item');
        let user = $('<div />').addClass('text-primary')
                                .text('Изпратено от: ' + c.Username)
                                .appendTo(commentContainer);

        let commentSeparator = $('<hr />').appendTo(commentContainer);
        let commentContent = $('<div />').text(c.Content).appendTo(commentContainer);
        let date = $('<div />').addClass('text-success')
                                .text('Дата: ' + extractDate(c.PostedDate)).appendTo(commentContainer);

        commentContainer.appendTo(container);
    })
}

function extractDate(date) {
    let parsedDate = new Date(Date.parse(date)).toLocaleDateString(),
        parsedTime = new Date(Date.parse(date)).toLocaleTimeString();

    return parsedDate + ' ' + parsedTime;
}