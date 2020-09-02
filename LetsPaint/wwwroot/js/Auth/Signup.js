$(document).on('change', '#signupEmail', function () {
    var $btnSubmit = $('#btnsubmit');
    $('.input-loading').show();
    $('.fa-times').hide();
    $('.fa-check').hide();
    api.http.post(url.root.auth.isEmailexist + '?email=' + $(this).val()).catch(apiError).then(function (data) {
        $('.input-loading').hide();
        if (data) {
            $('.fa-times').show();
            $('.fa-check').hide();
            $btnSubmit.prop('disabled', true).addClass('lp-btn-disable');
        }
        else {
            $('.fa-times').hide();
            $('.fa-check').show();
            $btnSubmit.prop('disabled', false).removeClass('lp-btn-disable');
        }
    });
});
