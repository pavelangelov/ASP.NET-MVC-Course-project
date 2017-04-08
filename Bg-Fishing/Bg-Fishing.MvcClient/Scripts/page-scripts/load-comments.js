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
        // Comment container
        let commentContainer = $('<li />').addClass('list-group-item');

        // Comment header
        let user = $('<div />').addClass('text-primary')
                                .text('Изпратено от: ' + c.Username)
                                .appendTo(commentContainer);

        let commentSeparator = $('<hr />').appendTo(commentContainer);

        // Comment body
        let commentBody = $('<div />');
        let commentContent = $('<div />').text(c.Content).appendTo(commentBody);
        let innerComments = $('<div />').attr('comments-for', c.Id);

        // Load inner comments
        c.Comments.forEach(innerComment => {
            let innerContainer = $('<div />').addClass('col-sm-8')
                                             .addClass('col-sm-offset-4')
                                             .addClass('inner-comment-container');
            // Inner comment header
            let user = $('<div />').addClass('text-success')
                                .addClass('inner-username')
                                .text('Отговор от : ' + innerComment.Username)
                                .appendTo(innerContainer);

            let commentSeparator = $('<hr />').appendTo(innerContainer);

            // Inner comment body
            let commentContent = $('<div />')
                                .addClass('inner-comment-body')
                                .text(innerComment.Content).appendTo(innerContainer);

            // Inner comment footer
            let date = $('<div />').addClass('text-success')
                                .addClass('inner-comment-date')
                                .text('Дата: ' + extractDate(innerComment.PostedDate))
                                .appendTo(innerContainer);

            innerContainer.appendTo(innerComments);
        })

        innerComments.appendTo(commentBody);
        commentBody.appendTo(commentContainer);

        // Comment footer
        let commentFooter = $('<div />').addClass('row');
        let date = $('<div />').addClass('text-success')
                                .addClass('col-sm-4')
                                .text('Дата: ' + extractDate(c.PostedDate))
                                .appendTo(commentFooter);

        let showResponseForm = $('<a />').addClass('btn')
                                            .addClass('btn-success')
                                            .addClass('show-response-form')
                                            .css('float', 'right')
                                            .attr('data-id', c.Id)
                                            .text('Отговори');

        let hideResponseForm = $('<a />').addClass('btn')
                                            .addClass('btn-danger')
                                            .addClass('hide-response-form')
                                            .css('float', 'right')
                                            .attr('data-id', c.Id)
                                            .hide()
                                            .text('Скрий');

        let responseContainer = $('<div />').addClass('right')
                                            .addClass('col-sm-8')
                                            .append(showResponseForm)
                                            .append(hideResponseForm)
                                            .appendTo(commentFooter);

        commentFooter.appendTo(commentContainer);
        commentContainer.appendTo(container);
    })
}

function extractDate(date) {
    let parsedDate = new Date(Date.parse(date)).toLocaleDateString(),
        parsedTime = new Date(Date.parse(date)).toLocaleTimeString();

    return parsedDate + ' ' + parsedTime;
}