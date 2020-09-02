$(document).ready(function () {
    $("#summary").dxValidationSummary({});
    let $file, $txtSuitable, $ddlGalleryType, $txtTitle, $txtBadge, $txtDescription, $ddlArtist, $txtSize, $numAvlQty, $txtMedium, $txtSurface, $numPrice, $txtTags, $chkIsAvl, $chkHasArtistSign, $chkHasArtistCertificate;
    var $query = app.methods.url.urlSearchParams();

    $file = $("#file-uploader").dxFileUploader({
        selectButtonText: "Select photo",
        labelText: "",
        accept: "image/*",
        uploadMode: "useForm", maxFileSize: 2000, allowedFileExtensions: [".jpg", ".jpeg", ".png"],
        allowCanceling: true,
        allowedFileExtensions: [],
        invalidFileExtensionMessage: "File type is not allowed",
        invalidMaxFileSizeMessage: "File is too large",
        invalidMinFileSizeMessage: "File is too small",
        isValid: true,
        labelText: "or Drop file here",
        maxFileSize: 0,
        minFileSize: 0,
    }).dxFileUploader("instance");

    $chkIsAvl = $("#chkIsAvl").dxCheckBox({
        value: true,
        width: 80,
        text: "Is Available"
    }).dxCheckBox('instance');

    $chkHasArtistSign = $("#chkHasArtistSign").dxCheckBox({
        value: true,
        width: 80,
        text: "Has Artist Sign"
    }).dxCheckBox('instance');

    $chkHasArtistCertificate = $("#chkHasArtistCertificate").dxCheckBox({
        value: true,
        width: 80,
        text: "Has Artist Certificate"
    }).dxCheckBox('instance');

    $txtTitle = $('#txtTitle').dxTextBox({
        value: "",
        maxLength: 50,
        placeholder: "Enter image Title",
        showClearButton: true
    }).dxValidator({
        validationRules: [{
            type: "required",
            message: "Image title is required."
        }]
    }).dxTextBox('instance');

    $txtSize = $('#txtSize').dxTextBox({
        value: "",
        maxLength: 50,
        placeholder: "Enter product size",
        showClearButton: true
    }).dxValidator({
        validationRules: [{
            type: "required",
            message: "Product size is required."
        }]
    }).dxTextBox('instance');





    $numAvlQty = $('#numAvlQty').dxNumberBox({
        value: "",
        min: 1,
        max: 1000,
        placeholder: "Enter available quantity",
        showClearButton: true
    }).dxValidator({
        validationRules: [{
            type: "required",
            message: "Available quantity is required."
        }]
    }).dxNumberBox('instance');

    $numPrice = $('#numPrice').dxNumberBox({
        value: "",
        min: 1,
        max: 9999999,
        placeholder: "Enter product price",
        showClearButton: true
    }).dxValidator({
        validationRules: [{
            type: "required",
            message: "Product price is required."
        }]
    }).dxNumberBox('instance');

    $txtBadge = $('#txtBadge').dxTextBox({
        value: "",
        maxLength: 20,
        placeholder: "Enter image badge",
        showClearButton: true
    }).dxTextBox('instance');

    $txtDescription = $('#txtDescription').dxTextArea({
        value: "",
        height: 113,
        maxLength: 500,
        placeholder: "Enter image description",
        showClearButton: true
    }).dxValidator({
        validationRules: [{
            type: "required",
            message: "Image description is required."
        }]
    }).dxTextArea('instance');
    api.http.get(apiURLs.admin.galleryManagement.getDropdownData + `?key=artist`).then(function (data) {
        $ddlArtist = $("#ddlArtist").dxSelectBox({
            dataSource: new DevExpress.data.ArrayStore({
                data: data,
                key: "value"
            }),
            displayExpr: "text",
            valueExpr: "value",
            value: data[0].value,
        }).dxSelectBox('instance');
    });
    api.http.get(apiURLs.admin.galleryManagement.getDropdownData + `?key=gallerytype`).then(function (data) {
        $ddlGalleryType = $("#ddlGalleryType").dxSelectBox({
            dataSource: new DevExpress.data.ArrayStore({
                data: data,
                key: "value"
            }),
            displayExpr: "text",
            valueExpr: "value",
            value: data[0].value,
        }).dxSelectBox('instance');
    });

    api.http.get(apiURLs.root.base.getReflookupData + `?key=Tags,ProductMedium,ProductSurface,Badge,ProductSuitable`).then(function (data) {
        $txtSurface = $('#txtSurface').dxTagBox({
            dataSource: new DevExpress.data.ArrayStore({
                data: data.filter(x => x.refKey === 'ProductSurface'),
                key: "refValue"
            }),
            displayExpr: "refValue",
            valueExpr: "refValue", acceptCustomValue: true,
            onCustomItemCreating: function (args) {
                var newValue = args.text,
                    component = args.component,
                    currentItems = component.option("items");
                currentItems.unshift(newValue);
                component.option("items", currentItems);
                args.customItem = newValue;
            },
            searchEnabled: true
        }).dxValidator({
            validationRules: [{
                type: "required",
                message: "Product surface is required."
            }]
        }).dxTagBox('instance');

        $txtSuitable = $('#txtSuitable').dxTagBox({
            dataSource: new DevExpress.data.ArrayStore({
                data: data.filter(x => x.refKey === 'ProductSuitable'),
                key: "refValue"
            }),
            displayExpr: "refValue",
            valueExpr: "refValue", acceptCustomValue: true,
            onCustomItemCreating: function (args) {
                var newValue = args.text,
                    component = args.component,
                    currentItems = component.option("items");
                currentItems.unshift(newValue);
                component.option("items", currentItems);
                args.customItem = newValue;
            },
            searchEnabled: true
        }).dxValidator({
            validationRules: [{
                type: "required",
                message: "Product suitable is required."
            }]
        }).dxTagBox('instance');


        $txtBadge = $('#txtBadge').dxTagBox({
            dataSource: new DevExpress.data.ArrayStore({
                data: data.filter(x => x.refKey === 'Badge'),
                key: "refValue"
            }),
            displayExpr: "refValue",
            valueExpr: "refValue", acceptCustomValue: true,
            onCustomItemCreating: function (args) {
                var newValue = args.text,
                    component = args.component,
                    currentItems = component.option("items");
                currentItems.unshift(newValue);
                component.option("items", currentItems);
                args.customItem = newValue;
            },
            searchEnabled: true
        }).dxTagBox('instance');
        $txtTags = $("#txtTags").dxTagBox({
            dataSource: new DevExpress.data.ArrayStore({
                data: data.filter(x => x.refKey === 'Tags'),
                key: "refValue"
            }),
            displayExpr: "refValue",
            valueExpr: "refValue", acceptCustomValue: true,
            onCustomItemCreating: function (args) {
                var newValue = args.text,
                    component = args.component,
                    currentItems = component.option("items");
                currentItems.unshift(newValue);
                component.option("items", currentItems);
                args.customItem = newValue;
            },
            searchEnabled: true
        }).dxTagBox('instance');
        $txtMedium = $('#txtMedium').dxTagBox({
            //value: "",
            //maxLength: 200,
            //placeholder: "Enter product medium",
            //showClearButton: true 
            dataSource: new DevExpress.data.ArrayStore({
                data: data.filter(x => x.refKey === 'ProductMedium'),
                key: "refValue"
            }),
            displayExpr: "refValue",
            valueExpr: "refValue", acceptCustomValue: true,
            onCustomItemCreating: function (args) {
                var newValue = args.text,
                    component = args.component,
                    currentItems = component.option("items");
                currentItems.unshift(newValue);
                component.option("items", currentItems);
                args.customItem = newValue;
            },
            searchEnabled: true
        }).dxValidator({
            validationRules: [{
                type: "required",
                message: "Product medium is required."
            }]
        }).dxTagBox('instance');

        $txtSurface = $('#txtSurface').dxTagBox({
            //value: "",
            //maxLength: 200,
            //placeholder: "Enter product medium",
            //showClearButton: true 
            dataSource: new DevExpress.data.ArrayStore({
                data: data.filter(x => x.refKey === 'ProductSurface'),
                key: "refValue"
            }),
            displayExpr: "refValue",
            valueExpr: "refValue", acceptCustomValue: true,
            onCustomItemCreating: function (args) {
                var newValue = args.text,
                    component = args.component,
                    currentItems = component.option("items");
                currentItems.unshift(newValue);
                component.option("items", currentItems);
                args.customItem = newValue;
            },
            searchEnabled: true
        }).dxValidator({
            validationRules: [{
                type: "required",
                message: "Product surface is required."
            }]
        }).dxTagBox('instance');


        if ($query && $query.get('galleryid')) {
            var $param = {
                pageNo: 1,
                pageSize: 10,
                id: $query.get('galleryid')
            };
            api.http.post(apiURLs.admin.galleryManagement.getGallery, $param).then(function (data) {
                var $data = data.data[0];
                $btn.option('text', 'Update')
                $('#imgPreview').attr('src', $data.baseUrl + $data.image);
                $txtSuitable.option('value', $data.suitableFor === null ? '' : $data.suitableFor.trim().split(','));
                $ddlGalleryType.option('value', $data.galleryTypeId);
                $txtTitle.option('value', $data.title);
                $txtBadge.option('value', $data.badge === null ? '' : $data.badge.trim().split(','));
                $txtDescription.option('value', $data.description);
                $ddlArtist.option('value', $data.artistId);
                $txtSize.option('value', $data.size);
                $numAvlQty.option('value', $data.availableQy);
                $txtMedium.option('value', $data.medium === null ? '' : $data.medium.trim().split(','));
                $txtSurface.option('value', $data.surface === null ? '' : $data.surface.trim().split(','));
                $numPrice.option('value', $data.price);
                $txtTags.option('value', $data.tags === null ? '' : $data.tags.trim().split(','));
                $chkIsAvl.option('value', $data.isAvailable);
                $chkHasArtistSign.option('value', $data.hasArtistSign);
                $chkHasArtistCertificate.option('value', $data.hasArtistCertificate);
            });
        }
    });
   


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



    $('#frmAddGallery').on('submit', function (e) {

        var $formData = new FormData();
        $formData.append('GalleryTypeId', $ddlGalleryType.option('value'));
        $formData.append('GalleryType', $ddlGalleryType.option('text'));
        $formData.append('file', $file.option('value')[0]);
        $formData.append('UserId',2);
        $formData.append('ArtistId',$ddlArtist.option('value'));
        $formData.append('AvailableQy',$numAvlQty.option('value'));
        $formData.append('Badge',$txtBadge.option('value'));
        $formData.append('Description',$txtDescription.option('value'));
        $formData.append('HasArtistCertificate',$chkHasArtistCertificate.option('value'));
        $formData.append('HasArtistSign',$chkHasArtistSign.option('value'));
        $formData.append('IsAvailable',$chkIsAvl.option('value'));
        $formData.append('Medium',$txtMedium.option('value'));
        $formData.append('Price',$numPrice.option('value'));
        $formData.append('Size',$txtSize.option('value'));
        $formData.append('SuitableFor',$txtSuitable.option('value'));
        $formData.append('Surface',$txtSurface.option('value'));
        $formData.append('Tags',$txtTags.option('value'));
        $formData.append('Title',$txtTitle.option('value'));
        if ($query) {
            $formData.append('galleryId',$query.get('galleryid'));
        }
        var $url = $btn.option('text') === 'Update' ? apiURLs.admin.galleryManagement.updateGallery : apiURLs.admin.galleryManagement.saveGallery;
        api.http.postWithFile($url, $formData).then(function (data) {
            api.successMsgHandler(data);
            app.methods.url.redirectTo(pageUrls.admin.galleryManagement.galleryList);
        }).catch(api.errorHandler);
        e.preventDefault();
    });

    $('.fas.fa-camera').click(function() {
        $('.dx-fileuploader-button.dx-button.dx-button-normal.dx-button-mode-contained.dx-widget.dx-button-has-text').click();
    });
});

window.addEventListener("beforeunload", function (e) {
    //var confirmationMessage = 'It looks like you have been editing something. '
    //    + 'If you leave before saving, your changes will be lost.';

    //(e || window.event).returnValue = confirmationMessage; //Gecko + IE
    //return confirmationMessage; //Gecko + Webkit, Safari, Chrome etc.
});