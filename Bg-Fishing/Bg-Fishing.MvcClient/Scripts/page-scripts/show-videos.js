'use strict';

$('#galleries').on('change', () => {
    var selectedValue = $('#galleries').val();
    var selectedTitle = $('#galleries option[value=' + selectedValue + ']').text();

    if (selectedValue !== undefined && selectedValue !== "") {
        $.ajax({
            method: 'GET',
            url: `/Galleries/GetVideos?galleryId=${selectedValue}`,
            success: (data) => {
                if (data !== null) {
                    var videos = JSON.parse(data);
                    $('#gallery-name').text(selectedTitle);
                    dispalyVideos(videos);
                }
            },
            error: (err) => {
                console.log(err);
            }
        })
    }
})

function dispalyVideos(videosArr) {
    var len = videosArr.length;
    var container = $('#videos-container');
    container.html('');
    var videosFragment = $('<div />');

    for (var i = 0; i < len; i += 1) {
        var video = videosArr[i];
        var videoContainer = $('<div />').addClass('col-sm-4');

        var title = $('<h4 />').addClass('text-warning').html(video.Title);
        var frame = $('<iframe />');
        var url = video.Url.replace("watch?v=", "embed/");
        frame.attr('src', url);

        videoContainer.append(title);
        videoContainer.append(frame);

        videoContainer.appendTo(videosFragment);
    }

    videosFragment.appendTo(container);
}