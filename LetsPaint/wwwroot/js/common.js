$(document).ready(function () {
    $(document).on('click', '.lp-viewpassword', function () {
        var $txtPass = $(this).parent().parent().find('input');
        if ($(this).find('i').hasClass('fa-eye')) {
            $($txtPass).attr('type', 'text');
        }
        else {
            $($txtPass).attr('type', 'password');
        }
        $(this).find('i').toggleClass('fa-eye-slash').toggleClass('fa-eye');

    });
});