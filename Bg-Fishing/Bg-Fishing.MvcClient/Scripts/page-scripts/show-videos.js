'use strict';

$('#galleries').on('change', () => {
    var selectedValue = $('#galleries').val();

    if (selectedValue !== undefined && selectedValue !== "") {
        let selectedTitle = $('#galleries option[value=' + selectedValue + ']').text();
        $.ajax({
            method: 'GET',
            url: `/Galleries/GetVideos?galleryId=${selectedValue}`,
            success: (data) => {
                if (data !== null) {
                    let videos = JSON.parse(data);
                    $('#gallery-name').text(selectedTitle);
                    dispalyVideos(videos);
                }
            },
            error: (err) => {
                console.log(err);
            }
        })
    } else {
        let container = $('#videos-container');
        container.html('');

        $('<li />').addClass('list-group-item')
                    .addClass('text-danger')
                    .addClass('text-center')
                    .text('В тази категория няма видеа!')
                    .appendTo(container);

        $('#gallery-name').text('');
    }
})

function dispalyVideos(videosArr) {
    let len = videosArr.length;
    let container = $('#videos-container');
    container.html('');
    let videosFragment = $('<div />');

    for (let i = 0; i < len; i += 1) {
        let video = videosArr[i];
        let videoContainer = $('<div />').addClass('col-sm-4');

        let id = video.Id;
        let videoLink = $('<a />').attr('href', '/galleries/watch?id=' + id).text(video.Title);
        let title = $('<h4 />').addClass('text-warning').append(videoLink);

        let videoId = video.Url.match(/youtube\.com.*(\?v=|\/embed\/)(.{11})/).pop();
        let thumb = $('<img class="thumb" src="//img.youtube.com/vi/' + videoId + '/0.jpg">').appendTo(videoContainer);

        videoContainer.append(title);
        videoContainer.append(thumb);

        videoContainer.appendTo(videosFragment);
    }

    videosFragment.appendTo(container);
}