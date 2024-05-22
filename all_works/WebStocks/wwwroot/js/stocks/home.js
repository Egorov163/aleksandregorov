
$(document).ready(function () {  

    $('.stock-name').click(function () {
        const details = $(this).closest('.block-stock-name').find('.details')
        details.toggleClass('short');
    });

})


//$('.delete-bond').addClass('short');
//$('.price-change-field').addClass('short');
/*const bondNameBlock = $(this).closest('.chapter-bond').find('.delete-bond');*/