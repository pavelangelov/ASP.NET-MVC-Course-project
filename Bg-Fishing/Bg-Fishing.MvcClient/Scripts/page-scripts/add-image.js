"use strict"

//=====================================================================================
//                   Load available galleries when lake is selected
//=====================================================================================
$('select#lakes-list').change((ev) => {
    let value = $('select#lakes-list option:selected').val(),
        text = $('select#lakes-list option:selected').text(),
        defaultValue = $('select#lakes-list option:first').val();

    if (value && value !== defaultValue) {
        // TODO: Get galleries from server and load them in select#galleries-list !!!
        $.get(`/image/getGalleries?lakeName=${text}`, (data) => {
            let result = JSON.parse(data),
                options = '<option>-----</option>';

            for (let i = 0, len = result.length; i < len; i += 1) {
                options += `<option value="${result[i]['Id']}">${result[i]['Name']}</option>`;
            };


            $('#galleries-list').removeAttr('disabled').html(options);
            $('#name-input').removeAttr('disabled');
        })
    } else {
        $('#galleries-list').html('').attr('disabled', '');
        $('#name-input').val('').attr('disabled', '');
    }
})

//=====================================================================================
//                                 Set gallery name
//=====================================================================================
$('select#galleries-list').change((ev) => {
    let value = $('select#galleries-list option:selected').val(),
        text = $('select#galleries-list option:selected').text(),
        defaultValue = $('select#galleries-list option:first').val();

    if (text !== undefined && text.length && text !== defaultValue) {
        $('input#name-input').val(text);
    } else {
        $('input#name-input').val('');
    }

})

//=====================================================================================
//                                  Validate uploaded file
//=====================================================================================
$('input[type=file]').on('change', (ev) => {
    readURL(ev.target);
})

const IMAGE_MAX_SIZE = 3 * 1024 * 1000,
    INVALID_FILE_TYPE_ERROR_MESSAGE = 'Избраният файл не е валидно изображение!',
    INVALID_SIZE_ERROR_MESSAGE = 'Избраният файл надхвърля разрешените 3MB!'

function readURL(input) {

    if (input.files && input.files[0]) {
        var reader = new FileReader();

        reader.onload = function (e) {
            $('img.img-preview').attr('src', e.target.result);
        }

        let errorContainer = $('div.error-messages');
        errorContainer.html('');

        if (input.files[0].type.indexOf('image/') < 0) {
            let err = $('<span />').html(INVALID_FILE_TYPE_ERROR_MESSAGE)
                .addClass('text-danger')
                .appendTo(errorContainer);
            return;
        }

        if (input.files[0].size > 3 * 1024 * 1000) {
            let err = $('<span />').html(INVALID_SIZE_ERROR_MESSAGE)
                .addClass('text-danger')
                .appendTo(errorContainer);
        }

        reader.readAsDataURL(input.files[0]);
    }
}

$("#imgInp").change(function () {
    readURL(this);
});