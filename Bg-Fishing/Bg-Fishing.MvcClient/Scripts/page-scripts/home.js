$('.container').on('click', 'a.show-more-btn', (ev) => {
    ev.preventDefault();
    let $this = $(ev.target);

    $($this.parent()).prev().css('max-height', 'inherit');
    $this.hide();
})

$(function () {
    var news = $.connection.newsHub;

    news.client.loadNews = function (news, hasMore, nextPage) {
        let container = $('#news-container');

        news.forEach((n) => {
            let newsContainer = $('<div />').addClass('col-sm-10')
                                            .addClass('col-sm-offset-1');
            let article = $('<article />').addClass('post');

            let thumb = $('<div />').addClass('post-thumb');
            let img = $('<img />').attr('src', n.ImageUrl)
                                    .attr('alt', 'News-Image')
                                    .appendTo(thumb);

            let content = $('<div />').addClass('post-content');
            let header = $('<header />').addClass('entry-header')
                                        .addClass('text-center')
                                        .addClass('text-uppercase');
            let h1 = $('<h1 />').addClass('entry-title')
                                .text(n.Title)
                                .appendTo(header);
            let entryContent = $('<div />').addClass('entry-content')
            let p = $('<p />').text(n.Content).appendTo(entryContent);
            let date = $('<div />').addClass('date')
                                    .text('Публикувано на: ' + n.PostedOn)
                                    .appendTo(entryContent);

            header.appendTo(content);
            entryContent.appendTo(content);
            $('<div />').addClass('show-more')
                        .html('<a class="btn show-more-btn">Виж повече...</a>')
                        .appendTo(content);

            thumb.appendTo(article);
            content.appendTo(article);

            article.appendTo(newsContainer);
            newsContainer.appendTo(container);
        })

        if (hasMore) {
            $('a.load-more-btn').attr('data-next-page', nextPage);
        } else {
            $('div#load-more').html('');
        }
    };

    // Start the connection.
    $.connection.hub.start().done(function () {
        $('.container').on('click', 'a.load-more-btn', (ev) => {
            let page = $(ev.target).attr('data-next-page');

            news.server.getNews(page);
        });
    });
});