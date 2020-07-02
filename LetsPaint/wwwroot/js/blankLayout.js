/// <reference path="api.js" />
$(document).on('change', '#signupEmail', function () {
    $('.input-loading').show();
    $('.fa-times').hide();
    $('.fa-check').hide();
    apiPost(url.root.auth.isEmailexist + '?email=' + $(this).val()).catch(apiError).then(function (data) {
        $('.input-loading').hide();
        if (data) {
            $('.fa-times').show();
            $('.fa-check').hide();
        }
        else {
            $('.fa-times').hide();
            $('.fa-check').show();
        }

    });

});

$(document).on('submit', '#frmChangePassword', function (e) {
    //e.stopPropagation();
    //e.preventDefault();
    //var param = {
    //    oldpassword:$('#oldpassword').val(),
    //    newpassword:$('#newpassword').val(),
    //    confirmpassword:$('#confirmpassword').val(),
    //};
    //apiPost(url.root.auth.changePassword, param).then(function(res) {

    //}).catch(apiError);
});

$('a[href*="#"]')
    // Remove links that don't actually link to anything
    .not('[href="#"]')
    .not('[href="#0"]')
    .click(function (event) {
        // On-page links
        if (
            location.pathname.replace(/^\//, '') == this.pathname.replace(/^\//, '')
            &&
            location.hostname == this.hostname
        ) {
            // Figure out element to scroll to
            var target = $(this.hash);
            target = target.length ? target : $('[name=' + this.hash.slice(1) + ']');
            // Does a scroll target exist?
            if (target.length) {
                // Only prevent default if animation is actually gonna happen
                event.preventDefault();
                $('html, body').animate({
                    scrollTop: target.offset().top
                }, 1000, function () {
                    // Callback after animation
                    // Must change focus!
                    var $target = $(target);
                    $target.focus();
                    if ($target.is(":focus")) { // Checking if the target was focused
                        return false;
                    } else {
                        $target.attr('tabindex', '-1'); // Adding tabindex for elements not focusable
                        $target.focus(); // Set focus again
                    };
                });
            }
        }
    });

$(document).on('click', '.profile-menu li:gt(0)', function () {
    $('.profile-menu ').hide();
});