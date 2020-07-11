$(document).on('change', '[data-type="mobile"]', function () {
    var $btnSubmit = $(this).parents().find('[type="submit"]');
    var phoneno = /^[0-9]{10}$/;

    if ($(this).val().trim() === '' || $(this).val().trim().match(phoneno)) {
        $btnSubmit.prop('disabled', false).removeClass('lp-btn-disable');
    }
    else {
        $btnSubmit.prop('disabled', true).addClass('lp-btn-disable');
    }
});

$(document).on('click', '#rmvPix', function () {
    $('#imgProfile').attr('src', '');
    $('#isImageRemoved').val('true');
});

$(document).on('click', '#addPix', function () {
    $('#photo').click();
});

$(document).on('change', '#photo', function (e) {
    for (var i = 0; i < e.originalEvent.srcElement.files.length; i++) {

        var file = e.originalEvent.srcElement.files[i];

        var reader = new FileReader();
        reader.onloadend = function () {
            $('#imgProfile').attr('src', reader.result);
        }
        reader.readAsDataURL(file);
    }
    
});