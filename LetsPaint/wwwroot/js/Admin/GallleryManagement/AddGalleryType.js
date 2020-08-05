$(document).ready(function () {
    $("#summary").dxValidationSummary({});
    let $txtGalleryType, $numGridSize, $txtBaseUrl;
    var $query = app.methods.url.urlSearchParams();
    $txtGalleryType = $('#txtGalleryType').dxTextBox({
        value: "",
        placeholder: "Enter gallery type",
        showClearButton: true
    }).dxValidator({
        validationRules: [{
            type: "required",
            message: " Gallery typeis required."
        }]
    }).dxTextBox('instance');

    $numGridSize = $('#numGridSize').dxNumberBox({
        value: "",
        placeholder: "Enter grid size",
        showClearButton: true
    }).dxValidator({
        validationRules: [{
            type: "required",
            message: "Grid size is required."
        }]
    }).dxNumberBox('instance');

    $txtBaseUrl = $('#txtBaseUrl').dxTextBox({
        value: "",
        placeholder: "Enter base url",
        showClearButton: true
    }).dxValidator({
        validationRules: [{
            type: "required",
            message: "Base URL is required."
        }]
    }).dxTextBox('instance');

    if ($query) {
        var $param = {
            pageNo: 1,
            pageSize: 10,
            id: $query.get('gallerytypeid')
        };
        api.http.post(apiURLs.admin.galleryManagement.getGalleryType, $param).then(function (data) {
            $numGridSize.option('value', data.data[0].gridSize);
            $txtBaseUrl.option('value', data.data[0].baseUrl);
            $txtGalleryType.option('value', data.data[0].galleryType);
            $txtBaseUrl.option('value', data.data[0].baseUrl);
            $btn.option('text', 'Update')
        });
    }

    var $btn = $("#btnSave").dxButton({
        icon: "save",
        type: "success",
        text: "Save",
        useSubmitBehavior: true,
        onClick: function () {
            if ($('.dx-validationsummary').children().length > 0) {
                $('#summary-container').show();
            }
        }
    }).dxButton('instance');



    $('#frmAddUser').on('submit', function (e) {

        var $formData = {};
        $formData.GalleryType= $txtGalleryType.option('value');
        $formData.GridSize=$numGridSize.option('value');
        $formData.BaseUrl=$txtBaseUrl.option('value');
        $formData.UserId = 2;
        if ($query) {
            $formData.galleryTypeId = $query.get('gallerytypeid');
            }
        var $url = $btn.option('text') === 'Update' ? apiURLs.admin.galleryManagement.updateGalleryType : apiURLs.admin.galleryManagement.saveGalleryType;
        api.http.post($url, $formData).then(function (data) {
            api.successMsgHandler(data);
                location.reload(true);
        }).catch(api.errorHandler);
        e.preventDefault();
    });
});