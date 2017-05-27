'use strict';

setTimeout(function () {
    $('p.result').fadeOut('fast');
}, 3000);

$('a.show-form').on('click', (ev) => {
    ev.preventDefault();
    $('form.response-form').show();
    $('a.show-form').hide();
});

$('a.hide-form').on('click', (ev) => {
    ev.preventDefault();
    $('form.response-form').hide();
    $('a.show-form').show();
})