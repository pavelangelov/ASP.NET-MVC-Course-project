$('.container').on('click', 'a.show-more-btn', (ev) => {
    ev.preventDefault();
    var $this = $(ev.target);
    console.log($this.parent());
    $($this.parent()).prev().css('max-height', 'inherit');
    $this.hide();
})