'use strict';

$('#comments').on('click', 'a.show-response-form', (ev) => {
    ev.preventDefault();
    let $this = $(ev.target),
        closeBtn = $this.siblings('.hide-response-form'),
        containerId = $this.attr('data-id'),
        commentsContainer = $('div[comments-for=' + containerId + ']'),
        parrent = $this.parent();

    showResponseForm(containerId, parrent);

    $this.hide();
    closeBtn.show();
})

$('#comments').on('click', 'a.hide-response-form', (ev) => {
    ev.preventDefault();
    let $this = $(ev.target),
        showBtn = $this.siblings('.show-response-form');

    $this.hide();
    showBtn.show();
    $this.siblings('.response-form').html('');
})

function showResponseForm(commentId, parrent) {
    let formContainer = $('<div />').addClass('response-form');

    let textArea = $('<textarea />').attr('id', 'content-for' + commentId)
                                    .addClass('form-control')
                                    .appendTo(formContainer);

    let btn = $('<a />').addClass('btn')
                        .addClass('btn-success')
                        .addClass('add-response')
                        .text('Добави')
                        .appendTo(formContainer);

    formContainer.appendTo(parrent);
}

$('#comments').on('click', 'a.add-response', (ev) => {
    let $this = $(ev.target),
        contentArea = $this.siblings('textarea'),
        content = contentArea.val(),
        commentId = contentArea.attr('id').replace('content-for', '');

    $.ajax({
        method: 'POST',
        url: `/api/comments/add/${commentId}/${content}`,
        success: (response) => {
            $('.load-comments').trigger('click');
            if (response != 'success') {
                console.log(response);
            }
        },
        error: (err) => {
            console.log(err);
        }
    })
})