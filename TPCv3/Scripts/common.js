$(function() {
    $('#search-form').submit(function() {
        if ($('#txtSearch').val().trim()) {
            return true;
        } else {
            return false;
        }
    });
    $('.fancybox').fancybox({
        helpers: {
            title: {
                type: 'outside'
            },
            thumbs: {
                width: 75,
                height: 75
            }
        },
        beforeLoad: function() {
            this.title = $(this.element).attr('data-caption');
        }
    });
    $('#da-slider').cslider({
        current: 0,
        // index of current slide

        bgincrement: 50,
        // increment the background position 
        // (parallax effect) when sliding

        autoplay: true,
        // slideshow on / off

        interval: 4000
        // time between transitions
    });
});