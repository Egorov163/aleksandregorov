
$(document).ready(function () {

    $('.stock-name').click(function () {
        $('.details').addClass('short');

        const details = $(this).closest('.block-stock-name').find('.details');
        details.removeClass('short');
    });

    $('.dollar').click(function () {
        var stockId = $(this).closest('.table-of-shares').attr('data-stock-id');
        var tableOfShares = $(this).closest('.table-of-shares');

        const url = '/StocksPortfolio/PricePlusOne?stockId=' + stockId;
        $.get(url)
            .then(function (response) {
                tableOfShares
                    .find('.price-count')
                    .text(response);
            });

    });
})
