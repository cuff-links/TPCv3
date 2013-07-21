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
function changeContactText(clickedLink) {
    var textToChange;
    switch (clickedLink) {
        case 'github':
            {
                textToChange = 'Github: www.github.com/silne30';
                break;
            }
        case 'email':
            {
                textToChange = 'Email: contact@thepowercoder.com';
                break;
            }
        case 'aim':
            {
                textToChange = 'AOL Instant Messenger: thepowercoder';
                break;
            }
        case 'skype':
            {
                textToChange = 'Skype: John.Dorlus';
                break;
            }
        case 'linkedin':
            {
                textToChange = 'LinkedIn: www.linkedin.com/in/johndorlus';
                break;
            }
        default:
            {
                textToChange = 'Google+: Silne.Dorlus@gmail.com';
            }
    }
    $('#contactLinkText').text(textToChange);
}
function submitButtonHover() {
    $('#sendEmailButton').css('color', '#000000');
    $('#sendEmailButton i').addClass('icon-black').removeClass('icon-white');
}
function submitButtonHoverStop() {
    $('#sendEmailButton').css('color', '#FFFFFF');
    $('#sendEmailButton i').removeClass('icon-black').addClass('icon-white');
}