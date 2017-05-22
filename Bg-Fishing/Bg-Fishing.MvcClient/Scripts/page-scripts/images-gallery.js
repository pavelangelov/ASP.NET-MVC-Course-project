'use strict';

$('#galleries-select').on('change', (ev) => {
    let value = $(ev.target).val(),
        defaultValue = $('select#galleries-select option:first').val();

    if (value && value != defaultValue) {
        let lakeName = $(`select#galleries-select option[value=${value}]`).text();
        $.get(`/api/images/galleries/${lakeName}`, (data) => {
            let galleries = JSON.parse(data).result,
                container = $('#lake-galleries-list'),
                list = $('<ul />').addClass('list-group');

            galleries.forEach(g => {
                let li = $('<li />').addClass('list-group-item');

                li.append(`<a href="#" class="gallery-link" data-id=${g['Id']} gallery-name=${g['Name']}>
                                <span class="glyphicon glyphicon-folder-open"></span>
                                <span class="gallery-name">${g['Name']}</span>
                                <span class="badge" title="Брой снимки в галерията">${g['ImagesCount']}</span>
                               </a>`);

                li.appendTo(list);
            });

            container.html(list);
            $('#images-list').html('');
        })
    }
})

$('#lake-galleries-list').on('click', 'a.gallery-link', (ev) => {
    let target = $(ev.target),
        galleryId = target.attr('data-id'),
        galleryName = target.attr('gallery-name');

    galleryId = galleryId || target.parent().attr('data-id');
    galleryName = galleryName || target.parent().attr('galleryName');

    if (!galleryId) {
        return;
    }

    $.get(`/api/images/gallery/${galleryId}`, (data) => {
        let images = JSON.parse(data).result,
            container = $('#images-list'),
            fragment = $('<div />');

        images.forEach(i => {
            $('<img />').attr('src', i['ImageUrl'])
                .appendTo(fragment);
        });

        if (images.length) {
            $("#current-image-container").html($('<img />').attr('src', images[0]['ImageUrl']));
        }

        container.html(fragment);
    })
})

$('#images-list').on('click', 'img', (ev) => {
    let url = $(ev.target).attr('src');

    if (url) {
        $("#current-image-container").html($('<img />').attr('src', url));
    }
})