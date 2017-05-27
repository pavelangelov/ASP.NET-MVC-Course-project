'use strict';

$('#video-upload').on('click', (ev) => {
        ev.preventDefault();
        var form = $('form');
        var token = $('input[name="__RequestVerificationToken"]', form).val();
        $.ajax({
            method: 'POST',
            url: '/moderator/video/add',
            data: {
                galleryId: $('#GalleryId').val(),
                newGalleryName: $('#NewGalleryName').val(),
                videoTitle: $('#VideoTitle').val(),
                videoUrl: $('#VideoUrl').val(),
                __RequestVerificationToken: token
                },
            cache: false,
            success: (data) => {
                if (data.status === 'success') {
                    $('#result').removeClass('text-danger').addClass('text-success').html(data.message);
                } else {
                    $('#result').removeClass('text-success').addClass('text-danger').html(data.message);
                }
            },
            error: (err) => {
                $('#result').removeClass('text-success').addClass('text-danger').html(err.message);
            }
        })
    })