'use strict';

$('#GalleriesSelect').on('change', () => {
    let selectedValue = $('#GalleriesSelect').val();

    if (selectedValue !== undefined && selectedValue !== "") {
        let selectedTitle = $('#GalleriesSelect option[value=' + selectedValue + ']').text();
        $.ajax({
            method: 'GET',
            url: `/api/Videos?galleryId=${selectedValue}`,
            success: (data) => {
                if (data !== null && data.length) {
                    $('#gallery-name').text(selectedTitle);
                    loadVideos(data, selectedTitle);
                } else {
                    showNoVideos();
                }
            },
            error: (err) => {
                console.log(err);
            }
        })
    } else {
        showNoVideos();
    }
})

$('#videos-container').on('click', 'a.delete-btn', (ev) => {
    let galleryName = $(ev.target).attr('data-galleryName'),
        videoId = $(ev.target).attr('data-videoId'),
        url = '/video/remove';

    $.ajax({
        method: 'POST',
        url: url,
        data: {
            galleryName,
            videoId
        },
        success: (response) => {
            if (response.status === 'success') {
                $('#GalleriesSelect').trigger('change');
            } else {
                $('#result').addClass('text-danger')
                            .text(response.message);

                setTimeout(() => {
                    $('#result').hide();
                }, 3000);
            }
        },
        error: (err) => {
            console.log(err);
        }
    })
})

function loadVideos(videosArr, galleryName) {
    let container = $('#videos-container');
    container.html('');

    videosArr.forEach((video) => {
        let videoContainer = $('<li />').addClass('list-group-item')
                                        .addClass('col-sm-4');

        let imageContainer = $('<div />').addClass('col-sm-7');

        let url = video.Url.replace("watch?v=", "embed/");
        let videoId = url.match(/youtube\.com.*(\?v=|\/embed\/)(.{11})/).pop();
        let thumb = $('<img class="thumb" src="//img.youtube.com/vi/' + videoId + '/0.jpg">').appendTo(imageContainer);

        let contentContainer = $('<div />').addClass('col-sm-5')
                                .addClass('text-center');
        let title = $('<h4 />').text(video.Title)
                                .appendTo(contentContainer);
        let deleteBtn = $('<a />').addClass('btn')
                                   .addClass('delete-btn')
                                   .attr('data-videoId', video.Id)
                                   .attr('data-galleryName', galleryName)
                                   .text('Премахни')
                                   .appendTo(contentContainer);

        imageContainer.appendTo(videoContainer);
        contentContainer.appendTo(videoContainer);
        container.append(videoContainer);
    })
}

function showNoVideos() {
    let message = $('<li />').addClass('text-center')
                                .addClass('list-group-item')
                                .addClass('text-danger')
                                .text('В тази категория няма видеа');

    $('#videos-container').html('').append(message);
}