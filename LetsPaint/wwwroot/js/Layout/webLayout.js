/// <reference path="api.js" />

//$(document).ready(function () {
//    $('.humbarger').on('click', function () {
//        $('.mobile-menu').toggleClass('hidden-menu');
//    });

//});
//$(window).resize(function () {
//    if ($(document).width() > 819) {
//        $('.mobile-menu').addClass('hidden-menu');
//    }
//});
window.onscroll = function () { scrollFunction() };

function scrollFunction() {
    if (window.innerWidth > 768) {
        if (document.body.scrollTop > 80 || document.documentElement.scrollTop > 80) {
            $("#navbar").css({ 'padding': '10px 15px', 'background': '#ffffff' });
            $('.menu .left-menu img').css({ 'padding': '5px 10px' });
        } else {
            $("#navbar").css({ 'padding': '25px 15px', 'background': '#ffffffc7' });
            $('.menu .left-menu img').css({ 'padding': '0px' });
        }
    }
    else {
        if (document.body.scrollTop > 80 || document.documentElement.scrollTop > 80) {
            $("#navbar").css({ 'background': '#ffffff' });
        } else {
            $("#navbar").css({ 'background': '#ffffffc7' });
        }

    }
}
$(document).mouseup(function (e) {
    if ($('.mobile-menu').is(':visible')) {
        $('.mobile-menu').addClass('mobile-menu-hidden');
        $('.humbarger').addClass('fa-bars').removeClass('fa-times');
    }
});
$(document).on('click', '.humbarger', function () {
    if ($('.mobile-menu').is(':visible')) {
        $(this).addClass('fa-bars').removeClass('fa-times');
        $('.mobile-menu').addClass('mobile-menu-hidden');
    } else {
        $(this).removeClass('fa-bars').addClass('fa-times'); $('.mobile-menu').removeClass('mobile-menu-hidden');
    }

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

$(document).on('click', 'a[href*="#"]:not([href="#0"]):not([href="#"])', function (event) {
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
    })

$(document).on('click', '.profile-menu li:gt(0)', function () {
    $('.profile-menu ').hide();
});

$(document).on('click', '.mobile-menu .user-logo', function () {
    if ($('.mobile-menu .mobile-submenu').is(':visible'))
        $('.mobile-menu .mobile-submenu').hide(1000);
    else {
        $('.mobile-menu .mobile-submenu').show(1000);
    }
});